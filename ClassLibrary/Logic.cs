

namespace ClassLibrary
{
    public class Logic
    {
        private List<Ship> Ships = new List<Ship>
        {
            new Ship("Vista", FlagColor.Green),
            new Ship("Kingslayer", FlagColor.Red),
            new Ship("ObraDinn", FlagColor.Blue)
        };

        private List<Ship> ShipsInBattle = new List<Ship>();

        public delegate void GameOverHandler();
        public event GameOverHandler? GameOverNotify;



        /// <summary>
        /// Создает объект корабля, добавляет его в общий список кораблей.
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="flagColor">Цвет</param>
        /// <returns>Объект корабля. Null, если входные данные некорректны.</returns>
        public Ship CreateShip(string name, object flagColor)
        {
            if (!string.IsNullOrWhiteSpace(name) && flagColor.ToString() != "_No_Color_")
            {
                Ship ship = new Ship(name, FlagColor.Black);
                SetFlagColor(ship, flagColor.ToString());
                Ships.Add(ship);
                return ship;
            }
            return null;
        }



        /// <summary>
        /// Удаляет объект корабля из общего списка кораблей.
        /// </summary>
        /// <param name="ship">Объект корабля.</param>
        public void DeleteShip(object ship) => Ships.Remove((Ship)ship);



        /// <summary>
        /// Возвращает объект корабля.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Объект искомого корабля. Null, если список кораблей пуст.</returns>
        public Ship GetShip(object ship)
        {
            if (Ships.Count == 0) 
            { 
                return null; 
            }

            return Ships[Ships.IndexOf((Ship)ship)];
        }



        /// <summary>
        /// Возвращает общий список кораблей.
        /// </summary>
        /// <returns>Общий список кораблей.</returns>
        public List<Ship> GetShipsList() => Ships;



        /// <summary>
        /// Возвращает список кораблей с ХП больше нуля (для игры).
        /// </summary>
        /// <returns>Список кораблей с ХП больше нуля.</returns>
        public List<Ship> GetShipsInBattleList() => ShipsInBattle;



        /// <summary>
        /// Создает новый список кораблей для новой игры.
        /// </summary>
        public void InitializeGame()
        {
            GameOverNotify = null;
            ShipsInBattle.Clear();

            if (Ships.Count != 0)
            {
                RecoverHP();
                ShipsInBattle = (from Ship ship in Ships select ship).ToList();
                PassTheTurn();
            }
        }



        /// <summary>
        /// Меняет название и цвет флага корабля.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <param name="name">Новое название корабля</param>
        /// <param name="flagColor">Название цвета флага</param>
        public void ChangeShipAttributes(object ship, string name, string flagColor)
        {
            if (string.IsNullOrWhiteSpace(name) && flagColor == "_No_Color_")
                return;

            if (flagColor == "_No_Color_" || (!string.IsNullOrWhiteSpace(name) && flagColor != "_No_Color_"))
                Ships[Ships.IndexOf((Ship)ship)].Name = name;

            if (string.IsNullOrWhiteSpace(name) || (!string.IsNullOrWhiteSpace(name) && flagColor != "_No_Color_"))
                SetFlagColor((Ship)ship, flagColor);
        }



        /// <summary>
        /// Возвращает список названий возможных цветов флага из перечисления цветов флага.
        /// </summary>
        /// <returns>Строковый список возможных цветов флага.</returns>
        public List<string> GetColorFlagNames() => Enum.GetNames(typeof(FlagColor)).ToList();



        /// <summary>
        /// Задает объекту корабля цвет флага по строке с названием цвета.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <param name="color">Название цвета</param>
        private void SetFlagColor(Ship ship, string color)
        {
            switch (color)
            {
                case "Red": ship.FlagColor = FlagColor.Red; break;
                case "Green": ship.FlagColor = FlagColor.Green; break;
                case "Blue": ship.FlagColor = FlagColor.Blue; break;
                case "Yellow": ship.FlagColor = FlagColor.Yellow; break;
                case "Pink": ship.FlagColor = FlagColor.Pink; break;
                case "Black": ship.FlagColor = FlagColor.Black; break;

                default: ship.FlagColor = FlagColor._No_Color_; break;
            }
        }



        /// <summary>
        /// Возвращает ХП корабля после атаки на него, соответственно меняет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>ХП корабля.</returns>
        public void AttackShipHP(object ship)
        {
            Ships[Ships.IndexOf((Ship)ship)].Hp -= 20;
            
            if (Ships[Ships.IndexOf((Ship)ship)].Hp <= 0)
            {
                ShipsInBattle.Remove((Ship)ship);
            }

            PassTheTurn();
        }



        /// <summary>
        /// Возвращает ХП корабля после его лечения, соответственно меняет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>ХП корабля.</returns>
        public void HealShipHP(object ship)
        {
            Ships[Ships.IndexOf((Ship)ship)].Hp += 15;
            PassTheTurn();
        }



        /// <summary>
        /// Проверяет, остался ли в игре один единственный корабль. Вызывает событие завершения игры.
        /// </summary>
        public void CheckShipsInBattle()
        {
            if (ShipsInBattle.Count <= 1)
            {
                PassTheTurn();
                GameOverNotify?.Invoke();   
            }
        }



        /// <summary>
        /// Передает ход следующему кораблю (игроку).
        /// </summary>
        private void PassTheTurn()
        {
            if (ShipsInBattle.Last().IsYourTurn == true)
            {
                ShipsInBattle.Last().IsYourTurn = false;
                ShipsInBattle[0].IsYourTurn = true;
                return;
            }

            foreach (Ship ship in ShipsInBattle)
            {
                if (ship.IsYourTurn == true)
                {
                    ship.IsYourTurn = false;
                    ShipsInBattle[ShipsInBattle.IndexOf(ship) + 1].IsYourTurn = true;
                    return;
                }
            }

            ShipsInBattle[0].IsYourTurn = true;
        }



        /// <summary>
        /// Возвращает корабль, который сейчас ходит.
        /// </summary>
        /// <returns>Объект корабля. Null, если корабль не найден.</returns>
        public Ship GetTurnShip()
        {
            foreach (Ship ship in ShipsInBattle) 
            {
                if (ship.IsYourTurn == true)
                {
                    return ship;
                }
            }

            return null;
        }



        /// <summary>
        /// Восстанавливает ХП всем кораблям.
        /// </summary>
        private void RecoverHP()
        {
            foreach (Ship ship in Ships)
            {
                ship.Hp = 100;
            }
        }   



        /// <summary>
        /// 
        /// </summary>
        public string GetHelpText()
        {
            string result = @"";
            using (var reader = new StreamReader(@"help.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result += line;
                    result += Environment.NewLine;
                }
                return result;
            }
        }
    }
}
