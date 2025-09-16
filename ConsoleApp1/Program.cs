using ClassLibrary;


namespace ConsoleApp1
{
    internal class Program
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

            Logic logic = new Logic();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ваш флот:");
                ShowShipsList(logic);

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
                        for (int i = 1; i < logic.GetColorFlagNames().Count; i++)
                        {
                            Console.WriteLine($"{i} - {logic.GetColorFlagNames()[i]}");
                        }

                        do
                        {
                            shipColor = Console.ReadLine()?.Replace(" ", "");
                            result = byte.TryParse(shipColor, out colorIndex);
                        }
                        while (result == false || colorIndex > logic.GetColorFlagNames().Count - 1 || colorIndex < 1);

                        Console.Clear();
                        logic.CreateShip(shipName, logic.GetColorFlagNames()[colorIndex]);
                        Console.WriteLine($"{shipName} построен!");
                        Console.ReadKey();
                        break;



                    case "2":
                        ShowShipsList(logic);

                        Console.WriteLine();
                        Console.WriteLine("Какой корабль удалить?");
                        do
                        {
                            input = Console.ReadLine()?.Replace(" ", "");
                            result = byte.TryParse(input, out shipIndex);
                        }
                        while (result == false || shipIndex > logic.GetShipsList().Count || shipIndex < 1);

                        Console.Clear();
                        Console.WriteLine($"Корабль {logic.GetShipsList()[shipIndex - 1].Name} потоплен!");
                        logic.DeleteShip(logic.GetShipsList()[shipIndex - 1]);
                        Console.ReadKey();
                        break;



                    case "3":
                        ShowShipsList(logic);

                        Console.WriteLine();
                        Console.WriteLine("Выберите корабль (по номеру)");
                        do
                        {
                            input = Console.ReadLine()?.Replace(" ", "");
                            result = byte.TryParse(input, out shipIndex);
                        }
                        while (result == false || shipIndex > logic.GetShipsList().Count || shipIndex < 1);

                        Console.WriteLine();
                        Console.WriteLine("Новое название корабля:");
                        Console.WriteLine("(enter, чтобы не менять)");
                        shipName = Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine("Цвет флага корабля:");
                        Console.WriteLine("0 - не менять");
                        for (int i = 1; i < logic.GetColorFlagNames().Count; i++)
                        {
                            Console.WriteLine($"{i} - {logic.GetColorFlagNames()[i]}");
                        }

                        do
                        {
                            shipColor = Console.ReadLine()?.Replace(" ", "");
                            result = byte.TryParse(shipColor, out colorIndex);
                        }
                        while (result == false || colorIndex > logic.GetColorFlagNames().Count - 1);

                        Console.Clear();
                        logic.ChangeShipAttributes(logic.GetShipsList()[shipIndex - 1], shipName, logic.GetColorFlagNames()[colorIndex]);
                        Console.WriteLine("Корабль изменен:");
                        Console.Write($"{logic.GetShipsList()[shipIndex - 1].Name} - ");
                        Console.Write(logic.GetShipsList()[shipIndex - 1].FlagColor);
                        Console.WriteLine();
                        Console.ReadKey();
                        break;



                    case "4":
                        logic.InitializeGame();

                        while (logic.GetShipsInBattleList().Count > 1)
                        {
                            Console.Clear();
                            Console.WriteLine($"Ход {logic.GetTurnShip().Name}");
                            Console.WriteLine();
                            for (int i = 0; i < logic.GetShipsInBattleList().Count(); i++)
                            {
                                Console.ForegroundColor = GetConsoleColorByFlagColor(logic, logic.GetShipsInBattleList()[i]);
                                Console.Write($"{i + 1} - {logic.GetShipsInBattleList()[i].Hp} HP - ");
                                Console.Write($"{logic.GetShipsInBattleList()[i].Name} - ");
                                Console.Write(logic.GetShipsInBattleList()[i].FlagColor);
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
                            while (result == false || shipIndex > logic.GetShipsInBattleList().Count || shipIndex < 1);

                            switch (commandNumber)
                            {
                                case "1": logic.AttackShipHP(logic.GetShipsInBattleList()[shipIndex - 1]); break;
                                case "2": logic.HealShipHP(logic.GetShipsInBattleList()[shipIndex - 1]); break;
                            }
                        }

                        Console.Clear();
                        logic.GetTurnShip();
                        Console.WriteLine($"Победа за {logic.GetTurnShip().Name}!!!");
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
        static void ShowShipsList(Logic logic)
        {
            for (int i = 0; i < logic.GetShipsList().Count(); i++)
            {
                Console.ForegroundColor = GetConsoleColorByFlagColor(logic, logic.GetShipsList()[i]);
                Console.Write($"{i + 1} - {logic.GetShipsList()[i].Hp} HP - ");
                Console.Write($"{logic.GetShipsList()[i].Name} - ");
                Console.Write(logic.GetShipsList()[i].FlagColor);
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
        static ConsoleColor GetConsoleColorByFlagColor(Logic logic, object ship)
        {
            switch (logic.GetShip(ship).FlagColor)
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
