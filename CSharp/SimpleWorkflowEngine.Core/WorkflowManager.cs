using System;
using System.IO;
using JetBrains.Annotations;
using Volante;

namespace SimpleWorkflowEngine.Core
{
    public class WorkflowManager:IDisposable
    {
        private readonly IDatabase _database;
        private bool _disposed;

        public WorkflowManager()
        {
            _database = DatabaseFactory.CreateDatabase();
            _database.Open(new NullFile());
            Workflow = new Workflow(_database);
            _database.Root = Workflow;
        }

        public WorkflowManager([NotNull] byte[] serializedWorkflow)
        {
            if (serializedWorkflow == null) throw new ArgumentNullException(nameof(serializedWorkflow));
            var memoryStream = new MemoryStream(serializedWorkflow);
            _database = DatabaseFactory.CreateDatabase();
            _database.Open(new StreamFile(memoryStream));
            Workflow = (Workflow) _database.Root;
        }

        public Workflow Workflow { get; }

        [NotNull]
        public byte[] Serialize()
        {
            using (var memStream = new MemoryStream())
            {
                _database.Backup(memStream);
                return memStream.ToArray();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _database.Close();
            _disposed = true;
        }
    }
}