using ClassLibrary;
using ClassLibrary.Interfaces;
using ClassLibrary.Logic;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Shared.ViewGame_EventArgs;
using Shared.ViewMain_EventArgs;
using Shared.WinForms_Interfaces;
using Shared.Console_Interfaces;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class Program : IConsoleMainView, IConsoleGameView
    {
        public event EventHandler<ViewMain_OnShipCreatedEventArgs> OnShipCreated;
        public event EventHandler<ViewMain_OnShipDeletedEventArgs> OnShipDeleted;
        public event EventHandler<ViewMain_OnShipNameChangedEventArgs> OnShipNameChanged;
        public event EventHandler<ViewMain_OnShipFlagColorChangedEventArgs> OnShipFlagColorChanged;
        public event Action OnShipListUpdated;
        public event Action OnFlagColorNamesRequested;

        public event EventHandler<ViewGame_OnShipAttackedEventArgs> OnShipAttacked;
        public event EventHandler<ViewGame_OnShipHealedEventArgs> OnShipHealed;
        public event Action OnShipsInBattleListUpdated;
        public event Action OnNewBattleStarted;
        public event Action OnPassTheTurn;
        public event Action OnGetTurnShip;



        /// <summary>
        /// Возвращает цвет типа ConsoleColor по названию цвета флага корабля
        /// </summary>
        /// <param name="flagColor">Название цвета флага</param>
        /// <returns>Цвет типа ConsoleColor</returns>
        static ConsoleColor GetConsoleColorByFlagColor(string flagColor)
        {
            switch (flagColor)
            {
                case "Red": return ConsoleColor.Red;
                case "Green": return ConsoleColor.Green;
                case "Blue": return ConsoleColor.Blue;
                case "Yellow": return ConsoleColor.Yellow;
                case "Pink": return ConsoleColor.Magenta;

                default: return ConsoleColor.Gray;
            }
        }



        public void InitializeFlagColorOptions(List<string> flagColorNames)
        {
            flagColors = flagColorNames;
        }



        public void CreateShip(string name, string flagColor)
        {
            OnShipCreated(this, new ViewMain_OnShipCreatedEventArgs(name, flagColor));
        }



        public void DeleteShip(string id)
        {
            OnShipDeleted(this, new ViewMain_OnShipDeletedEventArgs(id));
        }



        public void ChangeShipName(string id, string name)
        {
            OnShipNameChanged(this, new ViewMain_OnShipNameChangedEventArgs(id, name));
        }



        public void ChangeShipFlagColor(string id, string flagColor)
        {
            OnShipFlagColorChanged(this, new ViewMain_OnShipFlagColorChangedEventArgs(id, flagColor));
        }



        public void UpdateShipList(List<List<string>> shipsProperties)
        {
            ships = shipsProperties;
        }



        public void ShowShipList()
        {
            for (int i = 0; i < ships.Count; i++)
            {
                Console.ForegroundColor = GetConsoleColorByFlagColor(ships[i][2]);
                Console.Write($"{ships[i][3]}: {ships[i][0]} HP - ");
                Console.Write($"{ships[i][1]} - ");
                Console.Write($"{ships[i][2]}");
                Console.WriteLine();
                Console.ResetColor();
            }
        }



        public void UpdateShipsInBattle(List<List<string>> shipsProperties)
        {
            shipsInBattle = shipsProperties;
        }



        public void ShowShipsInBattle()
        {
            for (int i = 0; i < shipsInBattle.Count; i++)
            {
                Console.ForegroundColor = GetConsoleColorByFlagColor(shipsInBattle[i][2]);
                Console.Write($"{shipsInBattle[i][3]}: {shipsInBattle[i][0]} HP - ");
                Console.Write($"{shipsInBattle[i][1]} - ");
                Console.Write($"{shipsInBattle[i][2]}");
                Console.WriteLine();
                Console.ResetColor();
            }
        }



        public void AttackShip(string id)
        {
            OnShipAttacked(this, new ViewGame_OnShipAttackedEventArgs(id));
        }



        public void HealShip(string id)
        {
            OnShipHealed(this, new ViewGame_OnShipHealedEventArgs(id));
        }



        public void PassTheTurn()
        {
            OnPassTheTurn();
        }


        
        public void SetTurnShip()
        {
            OnGetTurnShip();
        }



        public void ChangeTurnShipName(string name)
        {
            turnShipName = name;
        }
        


        public void StartNewBattle()
        {
            OnShipsInBattleListUpdated();
            OnNewBattleStarted();
        }



        public void ResetGame()
        {
            StartNewBattle();
            SetTurnShip();
        }



        public string turnShipName;
        public List<string> flagColors;
        public List<List<string>> ships;
        public List<List<string>> shipsInBattle;



        /// <summary>
        /// Запускает консольное приложение
        /// </summary>
        public void Start()
        {
            OnFlagColorNamesRequested();
            
            string input;
            string shipId;
            string shipName;
            string colorIndex;
            string commandNumber;
            int shipIdParse;
            int colorIndexParse;
            bool result;
            bool isIdExists;
            
            while (true)
            {
                OnShipListUpdated();
                Console.Clear();
                Console.WriteLine("Ваш флот:");
                ShowShipList();

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
                        for (int i = 1; i < flagColors.Count; i++)
                        {
                            Console.WriteLine($"{i} - {flagColors[i]}");
                        }

                        do
                        {
                            colorIndex = Console.ReadLine()?.Replace(" ", "");
                            result = int.TryParse(colorIndex, out colorIndexParse);
                        }
                        while (result == false || colorIndexParse > flagColors.Count - 1 || colorIndexParse < 1);

                        CreateShip(shipName, flagColors[colorIndexParse]);

                        break;



                    case "2":
                        ShowShipList();
                        isIdExists = false;
                        Console.WriteLine();
                        Console.WriteLine("Какой корабль удалить?");
                        while (true)
                        {
                            do
                            {
                                shipId = Console.ReadLine().Trim();
                                result = int.TryParse(shipId, out shipIdParse);                              
                            }
                            while (result == false);

                            List<int> shipIds = new List<int>();
                            for(int i = 0; i < ships.Count; i++)
                            {
                                shipIds.Add(Convert.ToInt32(ships[i][3]));
                            }

                            foreach (int id in shipIds)
                            {
                                if (shipIdParse == id)
                                {
                                    isIdExists = true;
                                }
                            }

                            if (isIdExists == true) { break; }
                        }

                        DeleteShip(shipId);

                        break;



                    case "3":
                        ShowShipList();

                        Console.WriteLine();
                        Console.WriteLine("Выберите корабль (по Id)");

                        isIdExists = false;
                        while (true)
                        {
                            do
                            {
                                shipId = Console.ReadLine().Trim();
                                result = int.TryParse(shipId, out shipIdParse);
                            }
                            while (result == false);

                            List<int> shipIds = new List<int>();
                            for (int i = 0; i < ships.Count; i++)
                            {
                                shipIds.Add(Convert.ToInt32(ships[i][3]));
                            }

                            foreach (int id in shipIds)
                            {
                                if (shipIdParse == id)
                                {
                                    isIdExists = true;
                                }
                            }

                            if (isIdExists == true) { break; }
                        }

                        Console.WriteLine();
                        Console.WriteLine("Новое название корабля:");
                        Console.WriteLine("(enter, чтобы не менять)");
                        shipName = Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine("Цвет флага корабля:");
                        Console.WriteLine("0 - не менять");
                        for (int i = 1; i < flagColors.Count; i++)
                        {
                            Console.WriteLine($"{i} - {flagColors[i]}");
                        }

                        do
                        {
                            colorIndex = Console.ReadLine()?.Replace(" ", "");
                            result = int.TryParse(colorIndex, out colorIndexParse);
                        }
                        while (result == false || colorIndexParse > flagColors.Count - 1);

                        OnShipNameChanged(this, new ViewMain_OnShipNameChangedEventArgs(shipId, shipName));
                        OnShipFlagColorChanged(this, new ViewMain_OnShipFlagColorChangedEventArgs(shipId, flagColors[colorIndexParse]));

                        break;



                    case "4":
                        StartNewBattle();

                        while (shipsInBattle.Count > 1)
                        {
                            Console.Clear();
                            ShowShipsInBattle();
                            Console.WriteLine();
                            SetTurnShip();
                            Console.WriteLine($"Ходит {turnShipName}");


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
                            Console.WriteLine("Выберите корабль (по Id):");
                            Console.WriteLine();
                            isIdExists = false;
                            while (true)
                            {
                                do
                                {
                                    shipId = Console.ReadLine().Trim();
                                    result = int.TryParse(shipId, out shipIdParse);
                                }
                                while (result == false);

                                List<int> shipIds = new List<int>();
                                for (int i = 0; i < ships.Count; i++)
                                {
                                    shipIds.Add(Convert.ToInt32(ships[i][3]));
                                }

                                foreach (int id in shipIds)
                                {
                                    if (shipIdParse == id)
                                    {
                                        isIdExists = true;
                                    }
                                }

                                if (isIdExists == true) { break; }
                            }

                            switch (commandNumber)
                            {
                                case "1": OnShipAttacked(this, new ViewGame_OnShipAttackedEventArgs(shipId)); break;
                                case "2": OnShipHealed(this, new ViewGame_OnShipHealedEventArgs(shipId)); break;
                            }
                            PassTheTurn();
                            OnShipsInBattleListUpdated();
                        }
                        
                        Console.Clear();
                        PassTheTurn();
                        SetTurnShip();
                        Console.WriteLine($"{turnShipName} победил!!!");
                        Console.ReadKey();
                        ResetGame();
                        break;



                    default: break;
                }
            }
        }



        public static void Main(string[] args) { }
    }       
}
