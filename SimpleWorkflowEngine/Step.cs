using System;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine
{
    public sealed class Step : Persistent
    {
        protected internal Step()
        {
        }

        public Step([NotNull] string name, LogicOperand activationOperand = LogicOperand.And, StepState state = StepState.Waiting)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            ActivationOperand = activationOperand;
            Name = name;
            State = state;
        }

        public string Name { get; private set; }
        public StepState State { get; private set; }
        public LogicOperand ActivationOperand { get; private set; }

        public bool IsActive() => State == StepState.Active;
        public bool IsComplited() => State == StepState.Completed;

        public void SetState(StepState state)
        {
            State = state;
            Modify();
        }
    }
}