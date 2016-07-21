using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine.Core
{
    public class FlagsGroup : Persistent
    {
        protected internal FlagsGroup()
        {
        }

        public FlagsGroup([NotNull] IDatabase db, [NotNull] string id, LogicOperand logicOperand = LogicOperand.And,
            IDictionary<string, bool> flags = null) : base(db)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));
            if (id == null) throw new ArgumentNullException(nameof(id));
            Id = id;
            LogicOperand = logicOperand;
            Flags = db.CreateFieldIndex<string, Flag>("Id", IndexType.Unique);
            if (flags == null) return;
            AddFalgs(flags);
        }

        public string Id { get; }

        public LogicOperand LogicOperand { get; }

        public IFieldIndex<string, Flag> Flags { get; }

        public bool ContainsFlag([NotNull] string flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            return Flags.Get(flag) != null;
        }

        private void AddFalgs(IDictionary<string, bool> flags)
        {
            foreach (var flag in flags.Select(kvp => new Flag(kvp.Key, kvp.Value)))
            {
                Flags.Put(flag);
            }
        }

        public void AddFlag([NotNull] string flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            AddFlag(new Flag(flag));
        }

        public void AddFlag([NotNull] Flag flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            if (Flags.Put(flag)) return;
            throw new FlagAllReadyExistsException(flag.Id);
        }

        public void SetFlag([NotNull] string flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            var flagObject = Flags.Get(flag);
            if (flagObject == null) throw new FlagNotExistsException(flag);
            flagObject.Value = true;
            flagObject.Modify();
        }

        public bool MeetCondition()
        {
            if (Flags.Count == 0) return true;
            var values = Flags.Select(f => f.Value);
            return LogicOperand == LogicOperand.And ? values.All(v => v) : values.Any(v => v);
        }
    }
}