using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdgesController(
        IMemoryCache cache) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllEdges()
        {
            var edges = cache.Get<List<WorkflowEdge>>("edges");
            if (edges is null || edges.Count == 0)
                return NotFound("No edges found");

            return Ok(edges);
        }

        [HttpPost("create-edge")]
        public IActionResult CreateEdge(Guid fromNodeId, Guid toNodeId)
        {
            var nodes = cache.Get<List<WorkflowNode>>("nodes");

            var fromNode = nodes?.SingleOrDefault(n => n.Id == fromNodeId);
            var toNode = nodes?.SingleOrDefault(n => n.Id == toNodeId);

            if (fromNode is null || toNode is null)
                return NotFound("One or both nodes not found");

            var edges = cache.Get<List<WorkflowEdge>>("edges");

            if (edges is null)
            {
                edges = [];
                cache.Set("edges", edges);
            }

            var edge = new WorkflowEdge
            {
                FromNode = fromNodeId,
                ToNode = toNodeId
            };

            edges.Add(edge);

            return Ok(edge);
        }
    }
}
