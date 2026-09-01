using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OpsFlow.Domain.Entities;
using OpsFlow.Infrastructure.Engine;

namespace OpsFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkflowsController(IMemoryCache _cache) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateWorkflow([FromBody] Workflow workflow)
        {
            var flow = new Workflow
            {
                Id = Guid.NewGuid(),
                Name = workflow.Name,
                Nodes = workflow.Nodes,
                Edges = workflow.Edges
            };
            _cache.Set(flow.Id, flow);
            _cache.Set("workflows", _cache.Get<IEnumerable<Workflow>>("workflows")?.Append(flow) ?? new[] { flow });
            return Ok(new
            {
                Message = $"Workflow created successfully with ID: {flow.Id}",
                Flow = flow
            });
        }

        [HttpGet]
        public IActionResult GetWorkflows()
        {
            var workflows = _cache.Get<IEnumerable<Workflow>>("workflows");
            if (workflows == null)
            {
                return NotFound("No workflows found");
            }
            return Ok(workflows);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetWorkflowById(Guid id)
        {
            var workflow = _cache.Get<Workflow>(id);
            if (workflow == null)
            {
                return NotFound("Workflow not found");
            }
            return Ok(workflow);
        }

        [HttpPut("{id:guid}/definition")]
        public IActionResult UpdateDefinition(
    Guid id,
    [FromBody] WorkflowDefinitionRequest request)
        {
            var workflow = _cache.Get<Workflow>(id);

            if (workflow is null)
                return NotFound("Workflow not found");

            workflow.Nodes = request.Nodes;
            workflow.Edges = request.Edges;

            _cache.Set(id, workflow);

            return Ok(workflow);
        }

        [HttpGet("{id:guid}/definition")]
        public IActionResult GetWorkflowByIdWithDefinition(Guid id)
        {
            var workflow = _cache.Get<Workflow>(id);
            if (workflow == null)
            {
                return NotFound("Workflow not found");
            }
            return Ok(workflow);
        }

        [HttpPost("{id:guid}/execute")]
        public async Task<IActionResult> ExecuteWorkflow(
    Guid id,
    [FromServices] WorkflowEngine engine,
    CancellationToken cancellationToken)
        {
            var workflow = _cache.Get<Workflow>(id);

            if (workflow is null)
                return NotFound();

            await engine.ExecuteAsync(workflow, cancellationToken);

            return Ok(new
            {
                Message = "Workflow executed successfully"
            });
        }
    }
}
