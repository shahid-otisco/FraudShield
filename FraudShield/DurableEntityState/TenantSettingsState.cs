using FraudShield.Models;
using System.Threading.Tasks;

namespace FraudShield.DurableEntityState
{
    public interface ITenantSettingsState
    {
        Task<TenantSettings> GetSettingsAsync();
        Task SetSettingsAsync(TenantSettings settings);
    }
    public class TenantSettingsState : ITenantSettingsState
    {
        private TenantSettings _tenantSettings;

        public Task<TenantSettings> GetSettingsAsync()
        {
            return Task.FromResult(_tenantSettings);
        }

        public Task SetSettingsAsync(TenantSettings settings)
        {
            _tenantSettings = settings;
            return Task.CompletedTask;
        }
    }
}
