using System;

namespace FraudShield.Models
{
    public class TenantSettings
    {
        public string TenantId { get; set; }
        public DailyVelocityLimits VelocityLimits { get; set; }
        public TransactionThresholds Thresholds { get; set; }
        public CountrySanctions CountrySanctions { get; set; }
    }
}
