using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OpsFlow.Domain.Models.Results;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Infrastructure.Engine;

namespace OpsFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkflowsController(
        WorkflowEngine engine,
        IMemoryCache cache) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetWorkflows()
        {
            var workflows = cache.Get<List<Workflow>>("workflows");
            return Ok(workflows);
        }

        [HttpPost]
        public IActionResult CreateWorkflow([FromBody] Workflow workflow)
        {
            var workflows = cache.Get<List<Workflow>>("workflows") ?? new List<Workflow>();
            workflow.Id = Guid.NewGuid();
            workflows.Add(workflow);
            cache.Set("workflows", workflows);
            return CreatedAtAction(nameof(GetWorkflowById), new { id = workflow.Id }, workflow);
        }

        [HttpPost("{id:guid}/execute")]
        public async Task<IActionResult> ExecuteWorkflow(Guid id, CancellationToken cancellationToken)
        {
            // call executor to execute the workflow
            var workflows = cache.Get<List<Workflow>>("workflows");
            var workflow = workflows?.SingleOrDefault(w => w.Id == id);

            if (workflow is null)
                return NotFound($"Workflow with ID {id} not found");

            await engine.ExecuteAsync(workflow, cancellationToken);

            return Ok($"Workflow execution completed for {workflow.Id}");
        }


        // parallel via Task.WhenAll
        [HttpPost("execute-all")]
        public async Task<IActionResult> ExecuteAllWorkflows(
            CancellationToken cancellationToken)
        {
            var workflows = cache.Get<List<Workflow>>("workflows");

            if (workflows is null || workflows.Count == 0)
                return NotFound("No workflows found");

            var results = new List<WorkflowExecutionResult>();

            var startedAt = DateTimeOffset.UtcNow;

            foreach (var workflow in workflows)
            {
                try
                {
                    await engine.ExecuteAsync(workflow, cancellationToken);
                    var completedAt = DateTimeOffset.UtcNow;

                    results.Add(new WorkflowExecutionResult(
                        workflow.Id,
                        "Completed",
                        startedAt,
                        completedAt,
                        completedAt - startedAt));
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception)
                {
                    var completedAt = DateTimeOffset.UtcNow;
                    results.Add(new WorkflowExecutionResult(
                        workflow.Id,
                        "Failed",
                        startedAt,
                        completedAt,
                        completedAt - startedAt
                    ));
                }
            }

            return Ok(new
            {
                total = results.Count,
                completed = results.Count(x => x.Status == "Completed"),
                failed = results.Count(x => x.Status == "Failed"),
                executions = results,
                failedWorkflowIds = results.Where(w => w.Status == "Failed").Select(w => w.WorkflowId).ToList()
            });
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetWorkflowById(Guid id)
        {
            var workflows = cache.Get<List<Workflow>>("workflows");

            var workflow = workflows?.SingleOrDefault(w => w.Id == id);

            if (workflow is null)
                return NotFound($"Workflow with ID {id} not found");

            return Ok(workflow);
        }
    }
}
