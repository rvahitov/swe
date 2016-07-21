using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine.Core
{
    public class Transition : Persistent
    {
        protected internal Transition()
        {
        }

        public Transition([NotNull] IDatabase db, [NotNull] Step fromStep, [NotNull] Step toStep,
            LogicOperand transitionOperand = LogicOperand.And) : base(db)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));
            if (fromStep == null) throw new ArgumentNullException(nameof(fromStep));
            if (toStep == null) throw new ArgumentNullException(nameof(toStep));
            if (string.Equals(fromStep.Id, toStep.Id))
                throw new IllegalTransitionException(
                    $"Transition to the same step is not allowed. From step id '{fromStep.Id}'; To step id '{toStep.Id}'.");
            FromStep = fromStep;
            ToStep = toStep;
            TransitionOperand = transitionOperand;
            FlagsGroups = db.CreateFieldIndex<string, FlagsGroup>("Id", IndexType.Unique);
        }

        public Step FromStep { get; }
        public Step ToStep { get; }
        public LogicOperand TransitionOperand { get; }
        public IFieldIndex<string, FlagsGroup> FlagsGroups { get; }

        public bool ContainsFlagsGroup([NotNull] string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return FlagsGroups.Get(id) != null;
        }

        public FlagsGroup AddFlagsGroup([NotNull] string id, LogicOperand logicOperand = LogicOperand.And,
            IDictionary<string, bool> flags = null)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var group = new FlagsGroup(Database, id, logicOperand, flags);
            if (FlagsGroups.Put(group)) return group;
            throw new FlagsGroupAllReadyExistsException(id);
        }

        public void RemoveFlagsGroup([NotNull] string id)
        {
            if (FlagsGroups.RemoveKey(id) == null) throw new FlagsGroupNotExistsException(id);
        }

        public void AddFlag([NotNull] string flagsGroup, [NotNull] string flag, bool createGroup = false,
            LogicOperand groupOperand = LogicOperand.And)
        {
            if (flagsGroup == null) throw new ArgumentNullException(nameof(flagsGroup));
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            var group = FlagsGroups.Get(flagsGroup);
            if (group == null)
            {
                if(!createGroup) throw new FlagsGroupNotExistsException(flagsGroup);
                group = new FlagsGroup(Database,flagsGroup,groupOperand);
            }
            group.AddFlag(flag);
        }

        public void SetFlag([NotNull] string flagGroup, [NotNull] string flag)
        {
            if (flagGroup == null) throw new ArgumentNullException(nameof(flagGroup));
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            var group = FlagsGroups.Get(flagGroup);
            if(group == null) throw new FlagsGroupNotExistsException(flagGroup);
            group.SetFlag(flag);
        }
    }
}