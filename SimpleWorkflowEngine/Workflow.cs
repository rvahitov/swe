using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine
{
    public class Workflow : Persistent
    {
        public const string StartStepName = "StartStep";
        public const string EndStepName = "EndStep";

        protected internal Workflow()
        {
        }

        internal Workflow([NotNull] IDatabase db) : base(db)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));
            Steps = db.CreateFieldIndex<string, Step>("Name", IndexType.Unique);
            FromStepTransitions = db.CreateFieldIndex<string, Transition>("FromStepName", IndexType.NonUnique);
            ToStepTransitions = db.CreateFieldIndex<string, Transition>("ToStepName", IndexType.NonUnique);
            AllTransitions = db.CreateFieldIndex<Transition>(new[] {"FromStepName", "ToStepName"}, IndexType.Unique);
            Steps.Put(new Step(StartStepName));
            Steps.Put(new Step(EndStepName));
        }

        public IFieldIndex<string, Step> Steps { get; private set; }
        public IFieldIndex<string, Transition> FromStepTransitions { get; private set; }
        public IFieldIndex<string, Transition> ToStepTransitions { get; private set; }
        public IMultiFieldIndex<Transition> AllTransitions { get; }
        public Step StartStep => Steps.Get(StartStepName);
        public Step EndStep => Steps.Get(EndStepName);

        public Transition GetTransition([NotNull] string fromStep, [NotNull] string toStep)
        {
            if (fromStep == null) throw new ArgumentNullException(nameof(fromStep));
            if (toStep == null) throw new ArgumentNullException(nameof(toStep));
            return AllTransitions.Get(new object[] {fromStep, toStep});
        }

        public Transition GetTransition([NotNull] Step fromStep, [NotNull] Step toStep)
        {
            if (fromStep == null) throw new ArgumentNullException(nameof(fromStep));
            if (toStep == null) throw new ArgumentNullException(nameof(toStep));

            return GetTransition(fromStep.Name, toStep.Name);
        }

        public bool ContainsTransition([NotNull] string fromStep, [NotNull] string toStep)
        {
            if (fromStep == null) throw new ArgumentNullException(nameof(fromStep));
            if (toStep == null) throw new ArgumentNullException(nameof(toStep));
            return GetTransition(fromStep, toStep) != null;
        }

        public bool ContainsTransition([NotNull] Step fromStep, [NotNull] Step toStep)
        {
            if (fromStep == null) throw new ArgumentNullException(nameof(fromStep));
            if (toStep == null) throw new ArgumentNullException(nameof(toStep));
            return ContainsTransition(fromStep.Name, toStep.Name);
        }

        public IEnumerable<Step> ActiveSteps => Steps.Where(s => s.IsActive()).ToArray();

        public bool IsCompleted() => Steps.All(s => s.State == StepState.Completed || s.State == StepState.Skipped);

        public IEnumerable<Step> GetNextSteps([NotNull] Step fromStep)
        {
            if (fromStep == null) throw new ArgumentNullException(nameof(fromStep));
            return FromStepTransitions.Get(fromStep.Name, fromStep.Name).Select(t => t.ToStep).ToArray();
        }

        public IEnumerable<Step> GetPreviousSteps([NotNull] Step fromStep)
        {
            if (fromStep == null) throw new ArgumentNullException(nameof(fromStep));
            return ToStepTransitions.Get(fromStep.Name, fromStep.Name).Select(t => t.FromStep).ToArray();
        }

        [NotNull]
        public Step AddStep([NotNull] string name, StepState stepState = StepState.Waiting,
            LogicOperand activationOperand = LogicOperand.And)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            var step = new Step(name,activationOperand,stepState);
            if (Steps.Put(step)) return step;
            throw new StepAllReadyExistsException(name);
        }

        [NotNull]
        public Transition AddTransition(Step fromStep, Step toStep, LogicOperand transitionOperand = LogicOperand.And)
        {
            if(ContainsTransition(fromStep,toStep)) throw new TransitionAllReadyExistsException(fromStep.Name,toStep.Name);
            Steps.Put(fromStep);
            Steps.Put(toStep);
            var transition = new Transition(Database,fromStep,toStep,transitionOperand);
            AllTransitions.Put(transition);
            return transition;
        }
    }
}