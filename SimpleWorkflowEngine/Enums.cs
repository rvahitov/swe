using System;

namespace SimpleWorkflowEngine
{
    [Serializable] public enum LogicOperand { And, Or}
    [Serializable] public enum StepState { Waiting, Active, Completed, Skipped}
}