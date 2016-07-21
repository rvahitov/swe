using System;
using SimpleWorkflowEngine.Core;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            byte[] buffer;
            WorkflowManager workflowManager;
            using (workflowManager = new WorkflowManager())
            {
                var firstStep = new Step(Guid.NewGuid().ToString(), LogicOperand.And, StepState.Waiting);
                var secondStep = new Step(Guid.NewGuid().ToString(), LogicOperand.And, StepState.Waiting);
                workflowManager.Workflow.AddStep(firstStep);
                workflowManager.Workflow.AddStep(secondStep);
                var transaction = workflowManager.Workflow.AddTransition(firstStep, secondStep, LogicOperand.Or);
                var group = transaction.AddFlagsGroup("DEFAULT");
                group.AddFlag("Flag 1");
                group.AddFlag("Flag 2");
                buffer = workflowManager.Serialize();
            }
            using (workflowManager = new WorkflowManager(buffer))
            {
                
            }
        }
    }
}