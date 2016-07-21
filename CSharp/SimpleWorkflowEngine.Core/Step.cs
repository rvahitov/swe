using System;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine.Core
{
    public class Step : Persistent
    {
        protected internal Step()
        {
        }

        public Step([NotNull] string id, LogicOperand activationOperand, StepState state)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            Id = id;
            ActivationOperand = activationOperand;
            State = state;
        }

        public string Id { get; }

        public StepState State { get; }

        public LogicOperand ActivationOperand { get; }

        public bool IsActive() => State == StepState.Active;

        public bool IsCompleted() => State == StepState.Completed;
    }
}