using FraudShield.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudShield
{
    public class TenantSettingsProvider
    {
        public static TenantSettings GetTenantSettings(string tenantId)
        {
            // Simulate fetching tenant settings from storage or external service
            // Using a hard-coded sample settings object
            return new TenantSettings
            {
                TenantId = "345",
                VelocityLimits = new DailyVelocityLimits { Daily = 2500 },
                Thresholds = new TransactionThresholds { PerTransaction = 1500 },
                CountrySanctions = new CountrySanctions
                {
                    SourceCountryCode = new List<string> { "AFG", "BLR", "BIH", "IRQ", "KEN", "RUS" },
                    DestinationCountryCode = new List<string> { "AFG", "BLR", "BIH", "IRQ", "KEN", "RUS", "TKM", "UGA" }
                }
            };
        }
    }
}
