using ClassLibrary;
using Microsoft.Extensions.DependencyInjection;
using Model;


namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string? input;
            byte shipIndex;
            byte colorIndex;
            string? commandNumber;
            string? shipName;
            string? shipColor;
            bool result;

            ConfigModule configModule = new ConfigModule();
            ShipManager shipManager = configModule.serviceProvider.GetService<ShipManager>();
            ShipHPManager shipHPManager = configModule.serviceProvider.GetService<ShipHPManager>();
            ShipIsYourTurnManager shipIsYourTurnManager = configModule.serviceProvider.GetService<ShipIsYourTurnManager>();
            BattleManager battleManager = configModule.serviceProvider.GetService<BattleManager>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ваш флот:");
                ShowShipsList(shipManager);

                Console.WriteLine();
                Console.WriteLine("Делай корабли, йохохо");
                Console.WriteLine("1 - Построить корабль");
                Console.WriteLine("2 - Потопить корабль");
                Console.WriteLine("3 - Изменить корабль");
                Console.WriteLine("4 - Новая игра");
                Console.WriteLine();

                input = Console.ReadLine()?.Replace(" ", "");
                Console.Clear();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Название корабля:");
                        do
                        {
                            shipName = Console.ReadLine();
                        }
                        while (string.IsNullOrWhiteSpace(shipName));

                        Console.Clear();
                        Console.WriteLine("Цвет флага корабля:");
                        for (int i = 1; i < FlagColorManager.GetFlagColorNames().Count; i++)
                        {
                            Console.WriteLine($"{i} - {FlagColorManager.GetFlagColorNames()[i]}");
                        }

                        do
                        {
                            shipColor = Console.ReadLine()?.Replace(" ", "");
                            result = byte.TryParse(shipColor, out colorIndex);
                        }
                        while (result == false || colorIndex > FlagColorManager.GetFlagColorNames().Count - 1 || colorIndex < 1);

                        Console.Clear();
                        shipManager.CreateShip(shipName, FlagColorManager.GetFlagColorNames()[colorIndex]);
                        Console.WriteLine($"{shipName} построен!");
                        Console.ReadKey();
                        break;



                    case "2":
                        ShowShipsList(shipManager);

                        Console.WriteLine();
                        Console.WriteLine("Какой корабль удалить?");
                        do
                        {
                            input = Console.ReadLine()?.Replace(" ", "");
                            result = byte.TryParse(input, out shipIndex);
                        }
                        while (result == false || shipIndex > shipManager.GetShipsList().Count || shipIndex < 1);

                        Console.Clear();
                        Console.WriteLine($"Корабль {shipManager.GetShipsList()[shipIndex - 1].Name} потоплен!");
                        shipManager.DeleteShip(shipManager.GetShipsList()[shipIndex - 1]);
                        Console.ReadKey();
                        break;



                    case "3":
                        ShowShipsList(shipManager);

                        Console.WriteLine();
                        Console.WriteLine("Выберите корабль (по номеру)");
                        do
                        {
                            input = Console.ReadLine()?.Replace(" ", "");
                            result = byte.TryParse(input, out shipIndex);
                        }
                        while (result == false || shipIndex > shipManager.GetShipsList().Count || shipIndex < 1);

                        Console.WriteLine();
                        Console.WriteLine("Новое название корабля:");
                        Console.WriteLine("(enter, чтобы не менять)");
                        shipName = Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine("Цвет флага корабля:");
                        Console.WriteLine("0 - не менять");
                        for (int i = 1; i < FlagColorManager.GetFlagColorNames().Count; i++)
                        {
                            Console.WriteLine($"{i} - {FlagColorManager.GetFlagColorNames()[i]}");
                        }

                        do
                        {
                            shipColor = Console.ReadLine()?.Replace(" ", "");
                            result = byte.TryParse(shipColor, out colorIndex);
                        }
                        while (result == false || colorIndex > FlagColorManager.GetFlagColorNames().Count - 1);

                        Console.Clear();
                        shipManager.ChangeShipName(shipManager.GetShipsList()[shipIndex - 1], shipName);
                        shipManager.ChangeFlagColor(shipManager.GetShipsList()[shipIndex - 1], FlagColorManager.GetFlagColorNames()[colorIndex]);
                        Console.WriteLine("Корабль изменен:");
                        Console.Write($"{shipManager.GetShipsList()[shipIndex - 1].Name} - ");
                        Console.Write(shipManager.GetShipsList()[shipIndex - 1].FlagColor);
                        Console.WriteLine();
                        Console.ReadKey();
                        break;



                    case "4":
                        battleManager.InitializeNewBattle();

                        while (shipManager.GetNotDeadShipsList().Count > 1)
                        {
                            Console.Clear();
                            Console.WriteLine($"Ход {shipIsYourTurnManager.GetTurnShip().Name}");
                            Console.WriteLine();
                            for (int i = 0; i < shipManager.GetNotDeadShipsList().Count(); i++)
                            {
                                Console.ForegroundColor = GetConsoleColorByFlagColor(shipManager, shipManager.GetNotDeadShipsList()[i]);
                                Console.Write($"{i + 1} - {shipManager.GetNotDeadShipsList()[i].Hp} HP - ");
                                Console.Write($"{shipManager.GetNotDeadShipsList()[i].Name} - ");
                                Console.Write(shipManager.GetNotDeadShipsList()[i].FlagColor);
                                Console.WriteLine();
                                Console.ResetColor();
                            }

                            Console.WriteLine();
                            Console.WriteLine("Что прикажете?");
                            Console.WriteLine("1 - Атаковать");
                            Console.WriteLine("2 - Отремонитровать");
                            Console.WriteLine();
                            do
                            {
                                commandNumber = Console.ReadLine()?.Replace(" ", "");
                            }
                            while (commandNumber != "1" && commandNumber != "2");

                            Console.WriteLine();
                            Console.WriteLine("Выберите корабль (по номеру):");
                            Console.WriteLine();
                            do
                            {
                                input = Console.ReadLine()?.Replace(" ", "");
                                result = byte.TryParse(input, out shipIndex);
                            }
                            while (result == false || shipIndex > shipManager.GetNotDeadShipsList().Count || shipIndex < 1);

                            switch (commandNumber)
                            {
                                case "1": shipHPManager.AttackShipHP(shipManager.GetNotDeadShipsList()[shipIndex - 1]); break;
                                case "2": shipHPManager.HealShipHP(shipManager.GetNotDeadShipsList()[shipIndex - 1]); break;
                            }
                        }

                        Console.Clear();
                        shipIsYourTurnManager.GetTurnShip();
                        Console.WriteLine($"Победа за {shipIsYourTurnManager.GetTurnShip().Name}!!!");
                        Console.ReadKey();
                        break;



                    default: break;
                }
            }
        }



        /// <summary>
        /// Выводит на консоли список кораблей.
        /// </summary>
        /// <param name="logic">Объект бизнес-логики</param>
        static void ShowShipsList(ShipManager shipManager)
        {
            for (int i = 0; i < shipManager.GetShipsList().Count(); i++)
            {
                Console.ForegroundColor = GetConsoleColorByFlagColor(shipManager, shipManager.GetShipsList()[i]);
                Console.Write($"{i + 1} - {shipManager.GetShipsList()[i].Hp} HP - ");
                Console.Write($"{shipManager.GetShipsList()[i].Name} - ");
                Console.Write(shipManager.GetShipsList()[i].FlagColor);
                Console.WriteLine();
                Console.ResetColor();
            }
        }



        /// <summary>
        /// Возвращает цвет типа ConsoleColor по цвету флага корабля.
        /// </summary>
        /// <param name="logic">Объект бизнес-логики</param>
        /// <param name="ship">Объект корабля</param>
        /// <returns>Цвет типа ConsoleColor.</returns>
        static ConsoleColor GetConsoleColorByFlagColor(ShipManager shipManager, object ship)
        {
            switch (shipManager.GetShip(ship).FlagColor)
            {
                case FlagColor.Red: return ConsoleColor.Red;
                case FlagColor.Green: return ConsoleColor.Green;
                case FlagColor.Blue: return ConsoleColor.Blue;
                case FlagColor.Yellow: return ConsoleColor.Yellow;
                case FlagColor.Pink: return ConsoleColor.Magenta;

                default: return ConsoleColor.Gray;
            }
        }
    }
}
