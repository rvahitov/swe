using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine
{
    public sealed class FlagsGroup : Persistent
    {
        protected internal FlagsGroup()
        {
        }

        public FlagsGroup([NotNull] IDatabase db, [NotNull] string name, LogicOperand logicOperand = LogicOperand.And) : base(db)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));
            if (name == null) throw new ArgumentNullException(nameof(name));
            Flags = db.CreateFieldIndex<string, Flag>("Name", IndexType.Unique);
            Name = name;
            LogicOperand = logicOperand;
        }

        public string Name { get; private set; }

        public LogicOperand LogicOperand { get; private set; }

        public IFieldIndex<string, Flag> Flags { get; private set; }

        public bool ContainsFlag([NotNull] string flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            return Flags.Get(flag) != null;
        }

        public void AddFlags([NotNull] IEnumerable<string> flags)
        {
            if (flags == null) throw new ArgumentNullException(nameof(flags));
            foreach (var flag in flags)
            {
                Flags.Put(new Flag(flag));
            }
        }

        public void AddFlag([NotNull] string flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            if (!Flags.Put(new Flag(flag))) throw new FlagAllReadyExistsException(Name, flag);
        }

        public void SetFlag([NotNull] string flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            var fl = Flags.Get(flag);
            if (fl == null) throw new FlagNotExistsException(Name, flag);
            fl.Value = true;
            fl.Modify();
        }

        public bool Satisfies()
        {
            if (Flags.Count == 0) return true;
            var values = Flags.Select(f => f.Value).ToArray();
            switch (LogicOperand)
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