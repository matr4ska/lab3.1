using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ConfigModule
    {
        public ConfigModule() 
        {
            services = new ServiceCollection();

            services.AddSingleton<ShipManager>();
            services.AddSingleton<ShipHPManager>();
            services.AddSingleton<ShipIsYourTurnManager>();
            services.AddSingleton<BattleManager>();

            services.AddSingleton<IRepository<Ship>, EntityRepository<Ship>>();

            serviceProvider = services.BuildServiceProvider();
        }

        public IServiceCollection services;
        public IServiceProvider serviceProvider;
    }
}
