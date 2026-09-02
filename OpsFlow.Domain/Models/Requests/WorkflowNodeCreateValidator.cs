using FluentValidation;

namespace OpsFlow.Domain.Models.Requests
{
    public class WorkflowNodeCreateValidator : AbstractValidator<WorkflowNodeCreateRequest>
    {
        public WorkflowNodeCreateValidator()
        {
            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid node type.");
        }
    }
}
