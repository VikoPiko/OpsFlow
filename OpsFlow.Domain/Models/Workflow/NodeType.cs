namespace OpsFlow.Domain.Models.Workflow
{
    public enum NodeType
    {
        Start,
        Delay,
        Task,
        HttpRequest,
        Command,
        Log,
        End
    }
}
