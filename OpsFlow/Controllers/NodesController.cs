using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OpsFlow.Domain.Models.Requests;
using OpsFlow.Domain.Models.Results;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Infrastructure.Engine;
using System.Text.Json;

namespace OpsFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NodesController(
        NodeEngine nodeEngine,
        IMemoryCache cache) : ControllerBase
    {
        [HttpPost("create-node")]
        public IActionResult CreateNode(WorkflowNodeCreateRequest node)
        {
            if (node is null)
                return BadRequest("Node cannot be null");

            var workflowNode = new WorkflowNode
            {
                Id = Guid.NewGuid(),
                Type = node.Type,
                Name = node?.Name ?? $"Node-{Guid.NewGuid()}",
                Configuration = node?.Configuration ?? new JsonElement()
            };

            var nodes = cache.Get<List<WorkflowNode>>("nodes") ?? new List<WorkflowNode>();
            nodes.Add(workflowNode);
            cache.Set("nodes", nodes);

            return Ok(workflowNode);
        }

        [HttpGet]
        public IActionResult GetAllNodes(CancellationToken cancellationToken)
        {
            var nodes = cache.Get<List<WorkflowNode>>("nodes");

            if (nodes is null || nodes.Count == 0)
                return NotFound();

            return Ok(nodes);
        }

        [HttpPost("{id::guid}/execute")]
        public async Task<IActionResult> ExecuteNodeById(Guid id, CancellationToken cancellationToken)
        {
            var nodes = cache.Get<List<WorkflowNode>>("nodes");
            var node = nodes?.SingleOrDefault(n => n.Id == id);
            if (node is null)
                return NotFound();

            await nodeEngine.ExecuteNodeAsync(node, cancellationToken);

            return Ok();
        }

        [HttpPost("execute-all")]
        public async Task<IActionResult> ExecuteAllNodes(CancellationToken cancellationToken)
        {
            var nodes = cache.Get<List<WorkflowNode>>("nodes");
            if (nodes is null || nodes.Count == 0)
                return NotFound();

            var results = new List<NodeExecutionResult>();
            foreach (var node in nodes)
            {
                var startedAt = DateTimeOffset.UtcNow;

                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    await Task.Delay(1000, cancellationToken);
                    {
                        await nodeEngine.ExecuteNodeAsync(node, cancellationToken);
                        var completedAt = DateTimeOffset.UtcNow;
                        results.Add(new NodeExecutionResult
                        {
                            NodeId = node.Id,
                            StartedAt = startedAt,
                            CompletedAt = completedAt,
                            Status = "Completed",
                            Duration = completedAt - startedAt
                        });
                    }
                }
                catch (OperationCanceledException)
                    when (cancellationToken.IsCancellationRequested)
                {
                    var completedAt = DateTimeOffset.UtcNow;

                    results.Add(new NodeExecutionResult
                    {
                        NodeId = node.Id,
                        StartedAt = startedAt,
                        CompletedAt = completedAt,
                        Status = "Cancelled",
                        Duration = completedAt - startedAt
                    });
                    throw;
                }
                catch (Exception)
                {
                    var completedAt = DateTimeOffset.UtcNow;
                    results.Add(new NodeExecutionResult
                    {
                        NodeId = node.Id,
                        StartedAt = startedAt,
                        CompletedAt = completedAt,
                        Status = "Failed",
                        Duration = completedAt - startedAt
                    });
                }

            }
            return Ok(new
            {
                total = results.Count,
                nodeIds = nodes.Select(n => n.Id),
                failed = results.Count(x => x.Status == "Failed"),
                cancelled = results.Count(x => x.Status == "Cancelled"),
                completed = results.Count(x => x.Status == "Completed"),
                failedIds = results.Where(x => x.Status == "Failed").Select(x => x.NodeId).ToList()
            });
        }
    }
}
