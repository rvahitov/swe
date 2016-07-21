namespace SimpleWorkflowEngine.Core

type Step = {
    Id:string
    ActivationOperand:LogicOperand
    State:StepState
}
with 
    member me.IsActive with get() = me.State = StepState.Active
    member me.IsCompleted with get() = me.State = StepState.Completed
    member me.SetState(state) = {me with State = state}