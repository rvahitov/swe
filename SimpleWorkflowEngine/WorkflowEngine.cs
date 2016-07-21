using System;
using Volante;

namespace SimpleWorkflowEngine
{
    public class WorkflowEngine : IDisposable
    {
        private IDatabase _database;

        public Workflow Workflow { get; }
        public WorkflowEngine()
        {
            _database = DatabaseFactory.CreateDatabase();
            _database.Open(new NullFile());
            Workflow = new Workflow(_database);
            _database.Root = Workflow;
        }

        public void Dispose()
        {
            _database.Close();
        }
    }
}