using FraudShield.Models;

namespace FraudShield
{
    public class TransactionValidator
    {
        public static bool IsTransactionAllowed(TransactionEvent transactionEvent, TenantSettings tenantSettings)
        {
            // Check velocity limits
            if (IsVelocityLimitExceeded(transactionEvent, tenantSettings.VelocityLimits))
            {
                return false;
            }

            // Check payment thresholds
            if (IsPaymentThresholdExceeded(transactionEvent, tenantSettings.Thresholds))
            {
                return false;
            }

            // Check sanctions
            if (IsSanctioned(transactionEvent, tenantSettings.CountrySanctions))
            {
                return false;
            }

            // If none of the restrictions are violated, transaction is allowed
            return true;
        }

        private static bool IsVelocityLimitExceeded(TransactionEvent transactionEvent, DailyVelocityLimits velocityLimits)
        {
            // Assuming velocity limit is exceeded if the transaction amount exceeds the daily limit
            decimal totalAmountInDay = GetTotalAmountForDay(transactionEvent);
            if (totalAmountInDay > velocityLimits.Daily)
            {
                return true;
            }
            return false;
        }

        private static bool IsPaymentThresholdExceeded(TransactionEvent transactionEvent, TransactionThresholds thresholds)
        {
            // Assuming payment threshold is exceeded if the transaction amount exceeds the threshold
            if (transactionEvent.Amount > thresholds.PerTransaction)
            {
                return true;
            }
            return false;
        }

        private static bool IsSanctioned(TransactionEvent transactionEvent, CountrySanctions countrySanctions)
        {
            // Assuming any transaction involving source or destination country in sanctions list is sanctioned
            if (countrySanctions.SourceCountryCode.Contains(transactionEvent.SourceAccount.CountryCode) ||
                countrySanctions.DestinationCountryCode.Contains(transactionEvent.DestinationAccount.CountryCode))
            {
                return true;
            }
            return false;
        }

        private static decimal GetTotalAmountForDay(TransactionEvent transactionEvent)
        {
            // Assuming all transactions on the same day have the same amount
            return transactionEvent.Amount;
        }
    }
}
