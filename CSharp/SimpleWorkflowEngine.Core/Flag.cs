using System;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine.Core
{
    public class Flag : Persistent
    {
        protected internal Flag()
        {
            Id = "";
        }

        public Flag([NotNull] string id, bool value = false)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            Id = id;
            Value = value;
        }

        [NotNull]
        public string Id { get; }

        public bool Value { get; set; }
    }
}