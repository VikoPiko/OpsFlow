using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OpsFlow.Domain.Models.Requests;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Infrastructure.Engine;

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
                Configuration = node.Configuration
            };

            var nodes = cache.Get<List<WorkflowNode>>("nodes") ?? new List<WorkflowNode>();
            nodes.Add(workflowNode);
            cache.Set("nodes", nodes);

            return Ok(workflowNode);
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

            foreach (var node in nodes)
            {
                await nodeEngine.ExecuteNodeAsync(node, cancellationToken);
            }

            return Ok(new
            {
                nodeIds = nodes.Select(n => n.Id),
            });
        }
    }
}
