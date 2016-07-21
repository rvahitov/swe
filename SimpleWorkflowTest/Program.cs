using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWorkflowEngine;

namespace SimpleWorkflowTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var we = new WorkflowEngine())
            {
                var byWatermelon = we.Workflow.AddStep("Купить спелый арбуз");
                we.Workflow.AddTransition(we.Workflow.StartStep, byWatermelon,LogicOperand.And);
                var invitePetya = we.Workflow.AddStep("Пригласить Петю");
                we.Workflow.AddTransition(byWatermelon, invitePetya, LogicOperand.And)
                    .AddFlagsGroup("Арбуз")
                    .AddFlags(new[] {"спелый", "оплаченно"});
                var inviteVasya = we.Workflow.AddStep("Пригласить Васю");
                var inviteVasyaTransition = 
                we.Workflow.AddTransition(byWatermelon, inviteVasya, LogicOperand.And);
                inviteVasyaTransition
                    .AddFlagsGroup("Арбуз")
                    .AddFlags(new[] { "спелый", "оплаченно"});
                inviteVasyaTransition.AddFlagsGroup("Special").AddFlag("перейти_на_следующий_шаг");

                var inviteMasha = we.Workflow.AddStep("Пригласить Машу");
                we.Workflow.AddTransition(byWatermelon, inviteMasha, LogicOperand.And)
                    .AddFlagsGroup("Арбуз")
                    .AddFlags(new[] {"спелый", "оплаченно"});

                var cutWatermellon = we.Workflow.AddStep("Порезать арбуз", activationOperand: LogicOperand.Or);
                we.Workflow.AddTransition(invitePetya, cutWatermellon,LogicOperand.And)
                    .AddFlagsGroup("Приглашение")
                    .AddFlags(new[] {"Пете приглашение отправленно", "от Пети ответ получен"});
                we.Workflow.AddTransition(inviteVasya,cutWatermellon, LogicOperand.And)
                    .AddFlagsGroup("Приглашение")
                    .AddFlags(new[] { "Васе приглашение отправленно", "от Васи ответ получен" });
                we.Workflow.AddTransition(inviteMasha, cutWatermellon, LogicOperand.And)
                    .AddFlagsGroup("Приглашение",LogicOperand.Or)
                    .AddFlags(new[] { "Маше приглашение отправленно", "от Маши ответ получен" });
            }
        }
    }
}
