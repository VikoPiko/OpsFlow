namespace OpsFlow.Domain.Models.Results
{
    public sealed record NodeExecutionResult
    {
        public Guid NodeId;
        public DateTimeOffset StartedAt;
        public DateTimeOffset CompletedAt;
        public string? Status;
        public TimeSpan? Duration;
    }
}
