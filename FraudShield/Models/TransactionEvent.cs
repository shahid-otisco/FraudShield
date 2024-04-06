using System;

namespace FraudShield.Models
{
    public class TransactionEvent
    {
        public string CorrelationId { get; set; }
        public string TenantId { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Direction { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public AccountInfo SourceAccount { get; set; }
        public AccountInfo DestinationAccount { get; set; }
    }
}
