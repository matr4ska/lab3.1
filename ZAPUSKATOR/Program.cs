using ClassLibrary.Interfaces;
using ClassLibrary.Logic;
using ConfigModule;
using Microsoft.Extensions.DependencyInjection;
using Presenter.WinFormsPresenter;
using Presenter.ConsolePresenter;
using Shared;
using System.Diagnostics;
using System.Windows.Forms;
using WinFormsApp;
using ConsoleApp1;
using Presenter.WinFormsPresenter;


namespace ZAPUSKATOR
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConfigModule.ConfigModule configModule = new ConfigModule.ConfigModule();


            IShipManager shipManager = configModule.serviceProvider.GetService<IShipManager>();
            IShipHPManager shipHPManager = configModule.serviceProvider.GetService<IShipHPManager>();
            IShipIsYourTurnManager shipIsYourTurnManager = configModule.serviceProvider.GetService<IShipIsYourTurnManager>();
            IBattleManager battleManager = configModule.serviceProvider.GetService<IBattleManager>();
            IHelper helper = configModule.serviceProvider.GetService<IHelper>();
            IFlagColorManager flagColorManager = configModule.serviceProvider.GetService<IFlagColorManager>();


            string solutionPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName);
            string consolePath = @"ConsoleApp1\bin\Debug\net9.0\ConsoleApp1.exe";
            string fullPath = Path.Combine(solutionPath, consolePath);


            string input;


            Console.WriteLine("Выберите глазную боль:");
            Console.WriteLine("1 - WinForms");
            Console.WriteLine("2 - Console");


            do
            {
                input = Console.ReadLine().Trim();
            }
            while (input != "1" && input != "2");


            Console.Clear();


            switch(input)
            {
                case "1":
                    FormMain formMain = new FormMain();

                    MainPresenter mainPresenter = new MainPresenter(formMain, shipManager, helper, flagColorManager);
                    GamePresenter gamePresenter = new GamePresenter(formMain.formGame, shipManager, shipHPManager, shipIsYourTurnManager, battleManager);

                    Application.Run(formMain);

                    break;


                case "2":
                    ConsoleApp1.Program consoleView = new ConsoleApp1.Program();

                    MainConsolePresenter mainConsolePresenter = new MainConsolePresenter(consoleView, shipManager, helper, flagColorManager);
                    GameConsolePresenter gameConsolePresenter = new GameConsolePresenter(consoleView, shipManager, shipHPManager, shipIsYourTurnManager, battleManager);

                    consoleView.Start();

                    break;
            }
    

            Console.ReadKey();
        }
    }
}
