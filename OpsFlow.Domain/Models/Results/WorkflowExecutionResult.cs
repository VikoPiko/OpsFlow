namespace OpsFlow.Domain.Models.Results
{
    public sealed record WorkflowExecutionResult(
        Guid WorkflowId,
        string Status,
        DateTimeOffset StartedAt,
        DateTimeOffset CompletedAt,
        TimeSpan Duration);
}
