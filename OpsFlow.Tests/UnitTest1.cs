using OpsFlow.Domain.Exceptions;
using OpsFlow.Domain.Models.Workflow;
using System.Text.Json;

namespace OpsFlow.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void WorkflowNode_CreatesWithValidData()
        {
            var node = new WorkflowNode
            {
                Name = "Test Node",
                Type = NodeType.Log,
                Configuration = JsonDocument.Parse("{}").RootElement
            };

            // Act
            var result = WorkflowNode.Create(node);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal("Test Node", result.Name);
            Assert.Equal(NodeType.Log, result.Type);
            Assert.True(result.Configuration.HasValue);
        }

        [Fact]
        public void WorkflowNode_Create_ThrowsWhenNodeIsNull()
        {
            // Act
            var exception = Assert.Throws<DomainException>(
                () => WorkflowNode.Create(null!)
            );

            // Assert
            Assert.Equal("WorkflowNode cannot be null", exception.Message);
        }
    }
}
