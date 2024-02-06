using DailyCurrency.BackgroundService;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DailyCurrency.ScheduleTask
{
    public class RefreshCurrency : ScheduledProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RefreshCurrency(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override string Schedule => "*/1 * * * *"; // every 1 minute

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedServiceProvider = scope.ServiceProvider;
                var refreshCurrencyMethods = scopedServiceProvider.GetRequiredService<RefreshCurrencyMethods>();

                await refreshCurrencyMethods.DeleteAllData();
                await refreshCurrencyMethods.InsertCurrencyData();
            }

            Console.WriteLine("Updated daily currency : " + DateTime.UtcNow.ToString());

        }
    }
}
