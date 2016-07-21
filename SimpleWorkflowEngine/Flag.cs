using System;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine
{
    public sealed class Flag : Persistent
    {
        protected internal Flag()
        {
            Name = string.Empty;
            Value = false;
        }

        public Flag([NotNull] string name, bool value = false)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Name = name;
            Value = value;
        }

        [NotNull]
        public string Name { get; private set; }

        public bool Value { get; internal set; }
    }
}