namespace SimpleWorkflowEngine.Core

type LogicOperand = |And = 0 |Or = 1
type StepState = |Waiting = 0 |Active = 1 |Completed = 2 |Skipped = 3