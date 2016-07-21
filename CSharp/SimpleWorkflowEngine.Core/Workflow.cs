using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine.Core
{
    public class Workflow : Persistent
    {
        protected internal Workflow()
        {
        }

        public Workflow(IDatabase db) : base(db)
        {
            Steps = db.CreateFieldIndex<string, Step>("Id", IndexType.Unique);
            FromStepTransitions = db.CreateIndex<string, Transition>(IndexType.NonUnique);
            ToStepTransitions = db.CreateIndex<string, Transition>(IndexType.NonUnique);
        }

        public IFieldIndex<string, Step> Steps { get; }

        public IIndex<string, Transition> FromStepTransitions { get; }
        public IIndex<string, Transition> ToStepTransitions { get; }

        public void AddStep([NotNull] Step step)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));
            if (Steps.Put(step)) return;
            throw new StepAllReadyExistsException(step.Id);
        }

        public bool TransitionExists([NotNull] string fromStepId, [NotNull] string toStepId)
        {
            if (fromStepId == null) throw new ArgumentNullException(nameof(fromStepId));
            if (toStepId == null) throw new ArgumentNullException(nameof(toStepId));
            return FromStepTransitions.Get(fromStepId, fromStepId).Any(t => string.Equals(t.ToStep.Id, toStepId));
        }

        [NotNull]
        public Transition AddTransition([NotNull] Step fromStep, [NotNull] Step toStep,
            LogicOperand transitionOperand = LogicOperand.And)
        {
            if (fromStep == null) throw new ArgumentNullException(nameof(fromStep));
            if (toStep == null) throw new ArgumentNullException(nameof(toStep));
            var transition = new Transition(Database, fromStep, toStep, transitionOperand);
            AddTransition(transition);
            return transition;
        }

        public void AddTransition([NotNull] Transition transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));
            if (TransitionExists(transition.FromStep.Id, transition.ToStep.Id))
                throw new TransitionAllReadyExistsException(transition.FromStep.Id, transition.ToStep.Id);
            FromStepTransitions.Put(transition.FromStep.Id, transition);
            ToStepTransitions.Put(transition.ToStep.Id, transition);
            Steps.Put(transition.FromStep);
            Steps.Put(transition.ToStep);
        }

        [NotNull]
        public IEnumerable<Transition> GetTransitionsFromStep([NotNull] string fromStepId)
        {
            if (fromStepId == null) throw new ArgumentNullException(nameof(fromStepId));
            return FromStepTransitions.Get(fromStepId, fromStepId).ToArray();
        }

        [NotNull]
        public IEnumerable<Transition> GetTransitionsToStep([NotNull] string toStepId)
        {
            if (toStepId == null) throw new ArgumentNullException(nameof(toStepId));
            return ToStepTransitions.Get(toStepId, toStepId).ToArray();
        }

        public IEnumerable<Step> ActiveSteps()
        {
            return Steps.Where(s => s.IsActive()).ToArray();
        } 
    }
}