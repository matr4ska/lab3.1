using ClassLibrary.Interfaces;
using ClassLibrary.Logic;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Model;
using WinFormsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using WinFormsApp;
using Presenter.WinFormsPresenter;

namespace ConfigModule
{
    public class ConfigModule
    {
        public ConfigModule() 
        {
            services = new ServiceCollection();

            services.AddSingleton<IShipManager, ShipManager>();
            services.AddSingleton<IShipHPManager, ShipHPManager>();
            services.AddSingleton<IShipIsYourTurnManager, ShipIsYourTurnManager>();
            services.AddSingleton<IBattleManager, BattleManager>();
            services.AddSingleton<IHelper, Helper>();
            services.AddSingleton<IFlagColorManager, FlagColorManager>();

            services.AddSingleton<IRepository<Ship>, EntityRepository<Ship>>();

            serviceProvider = services.BuildServiceProvider();
        }

        public IServiceCollection services;
        public IServiceProvider serviceProvider;
    }
}
