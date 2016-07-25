using System.Collections.Generic;
using MrCMS.Services.Resources;
using MrCMS.Settings;

namespace MrCMS.HealthChecks
{
    public class SiteIsLive : HealthCheck
    {
        private readonly SiteSettings _siteSettings;

        public SiteIsLive(SiteSettings siteSettings)
        {
            _siteSettings = siteSettings;
        }

        public override string DisplayName
        {
            get { return "Site in live mode"; }
        }

        public override HealthCheckResult PerformCheck()
        {
           
            if (!_siteSettings.SiteIsLive)
            {
                return new HealthCheckResult
                {
                    Messages = new List<string> { "The current site is not set to live, please change this in site settings." },
                    OK = false
                };
            }
            return HealthCheckResult.Success;
        }
    }

    public class OptimizationsEnabled : HealthCheck
    {
        private readonly BundlingSettings _bundlingSettings;
        private readonly IStringResourceProvider _stringResourceProvider;

        public OptimizationsEnabled(BundlingSettings bundlingSettings, IStringResourceProvider stringResourceProvider)
        {
            _bundlingSettings = bundlingSettings;
            _stringResourceProvider = stringResourceProvider;
        }

        public override HealthCheckResult PerformCheck()
        {
            if (_bundlingSettings.EnableOptimisations)
                return new HealthCheckResult
                {
                    OK = true,
                    Messages = new List<string> {_stringResourceProvider.GetValue("Optimizations enabled"),}
                };

            return new HealthCheckResult
            {
                OK = false,
                Messages = new List<string>
                {
                    _stringResourceProvider.GetValue("Please enable optimizations in system settings.")
                }
            };
        }
    }
}