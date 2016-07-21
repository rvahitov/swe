using System;
using System.Linq;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine
{
    public sealed class Transition : Persistent
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
            FlagsGroups = db.CreateFieldIndex<string, FlagsGroup>("Name", IndexType.Unique);
            FromStep = fromStep;
            ToStep = toStep;
            TransitionOperand = transitionOperand;
        }

        public Step FromStep { get; private set; }
        public Step ToStep { get; private set; }
        public LogicOperand TransitionOperand { get; }
        public IFieldIndex<string, FlagsGroup> FlagsGroups { get; }

        public string FromStepName => FromStep.Name;
        public string ToStepName => ToStep.Name;

        public bool ContainsFlagsGroup([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return FlagsGroups.Get(name) != null;
        }

        [NotNull]
        public FlagsGroup AddFlagsGroup([NotNull] string name, LogicOperand groupLogicOperand = LogicOperand.And)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            var fg = FlagsGroups.Get(name);
            if (fg != null) throw new FlagsGroupAllReadyExistsException(name);
            fg = new FlagsGroup(Database, name, groupLogicOperand);
            FlagsGroups.Put(fg);
            return fg;
        }

        public void AddFlag([NotNull] string flagsGroup, [NotNull] string flag, bool createGroup = false,
            LogicOperand groupOperand = LogicOperand.And)
        {
            if (flagsGroup == null) throw new ArgumentNullException(nameof(flagsGroup));
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            var fg = FlagsGroups.Get(flagsGroup);
            if (fg == null && createGroup)
            {
                fg = new FlagsGroup(Database, flagsGroup, groupOperand);
                FlagsGroups.Put(fg);
            }
            else if (fg == null)
            {
                throw new FlagsGroupNotExistsException(flagsGroup);
            }
            fg.AddFlag(flag);
        }

        public void SetFlag([NotNull] string flagsGroup, [NotNull] string flag)
        {
            if (flagsGroup == null) throw new ArgumentNullException(nameof(flagsGroup));
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            var fg = FlagsGroups.Get(flagsGroup);
            if (fg == null) throw new FlagsGroupNotExistsException(flagsGroup);
            fg.SetFlag(flag);
        }

        public void SetFlagForAllGroups([NotNull] string flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            foreach (var flagsGroup in FlagsGroups.Where(fg => fg.ContainsFlag(flag)))
            {
                flagsGroup.SetFlag(flag);
            }
        }

        public bool Satisfies()
        {
            if (FlagsGroups.Count == 0) return true;
            var values = FlagsGroups.Select(fg => fg.Satisfies()).ToArray();
            switch (TransitionOperand)
            {
                case LogicOperand.And:
                    return values.All(v => v);
                case LogicOperand.Or:
                    return values.Any(v => v);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}