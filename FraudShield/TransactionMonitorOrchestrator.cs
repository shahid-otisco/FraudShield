using FraudShield.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FraudShield
{
    public static class TransactionMonitorOrchestrator
    {
        [FunctionName("TransactionMonitorOrchestrator")]
        public static async Task RunOrchestrator([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var incomingMessage = context.GetInput<string>();
            var transactionEvent = JsonConvert.DeserializeObject<TransactionEvent>(incomingMessage);

            var tenantSettings = TenantSettingsProvider.GetTenantSettings(transactionEvent.TenantId);

            // Assess the incoming message against the restrictions defined in the Tenant settings
            if (!TransactionValidator.IsTransactionAllowed(transactionEvent, tenantSettings))
            {
                // Raise an event and send the payment to a holding queue for assessment
                await context.CallActivityAsync("RaiseEventAndSendToHoldingQueue", transactionEvent);
            }
            else
            {
                // Send the payment to processing queue if no tenant settings are violated
                await context.CallActivityAsync("SendToProcessingQueue", transactionEvent);
            }
        }


        [FunctionName("RaiseEventAndSendToHoldingQueue")]
        public static async Task RaiseEventAndSendToHoldingQueue([ActivityTrigger] TransactionEvent transactionEvent)
        {
            // Raise an event and send the payment to a holding queue for assessment
            Console.WriteLine("Transaction violated tenant settings. Sending payment to holding queue.");
        }


        [FunctionName("SendToProcessingQueue")]
        public static async Task SendToProcessingQueue([ActivityTrigger] TransactionEvent transactionEvent)
        {
            // Send the payment to processing queue
            Console.WriteLine("Transaction passed tenant settings. Sending payment to processing queue.");
        }
    }
}