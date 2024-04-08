using FraudShield.DurableEntityState;
using FraudShield.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;

namespace FraudShield
{
    public static class TransactionMonitorOrchestrator
    {
        [FunctionName("TransactionMonitorOrchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            [DurableClient] IDurableEntityClient entityClient,
            [QueueTrigger("incomingtransactions")] TransactionEvent transactionEvent,
            [Queue("processingqueue")] IAsyncCollector<TransactionEvent> processingQueue,
            [Queue("holdingqueue")] IAsyncCollector<TransactionEvent> holdingQueue)
        {
            var tenantId = transactionEvent.TenantId;

            //Read tenant settings from duralbe state.
            var entityId = new EntityId(nameof(TenantSettingsState), tenantId);
            var stateResponse = await entityClient.ReadEntityStateAsync<TenantSettingsState>(entityId);
            var tenantSettings = await stateResponse.EntityState?.GetSettingsAsync() ?? TenantSettingsProvider.GetTenantSettings(tenantId);

            // Assess the incoming message against the restrictions defined in the Tenant settings
            if (!TransactionValidator.IsTransactionAllowed(transactionEvent, tenantSettings))
            {
                // Raise an event and send the payment to a holding queue for assessment
                await holdingQueue.AddAsync(transactionEvent);
            }
            else
            {
                // Send the payment to processing queue if no tenant settings are violated
                await processingQueue.AddAsync(transactionEvent);
            }

            // Save the updated tenantSettings back to the entity state
            await entityClient.SignalEntityAsync<ITenantSettingsState>(entityId, proxy => proxy.SetSettingsAsync(tenantSettings));
        }
    }
}