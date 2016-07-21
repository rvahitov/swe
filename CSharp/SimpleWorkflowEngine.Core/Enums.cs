namespace SimpleWorkflowEngine.Core
{
    public enum LogicOperand { And, Or }

    public enum StepState
    {
        Waiting,
        Active,
        Completed,
        Skipped
    }
}