using Model;
using DataAccessLayer;
using System.Data.SqlClient;

namespace ClassLibrary
{
    public class Logic
    {
        public Logic()
        {
            repository = new DapperRepository<Ship>();
        }

        private IRepository<Ship> repository;
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
                Ship ship = new Ship()
                {
                    Name = name,
                    Hp = 100,
                    FlagColor = FlagColor.Black,
                    IsYourTurn = false,
                };
                SetFlagColor(ship, flagColor.ToString());

                repository.Create(ship);
                return ship;
            }
            return null;
        }



        /// <summary>
        /// Удаляет объект корабля из общего списка кораблей.
        /// </summary>
        /// <param name="ship">Объект корабля.</param>
        public void DeleteShip(object ship)
        {
            Ship _ship = (Ship)ship;
            repository.Delete(_ship.Id);
        }



        /// <summary>
        /// Возвращает объект корабля.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Объект искомого корабля. Null, если список кораблей пуст.</returns>
        public Ship GetShip(object ship)
        {
            return repository.GetItem(((Ship)ship).Id);
        }



        /// <summary>
        /// Возвращает общий список кораблей.
        /// </summary>
        /// <returns>Общий список кораблей.</returns>
        public List<Ship> GetShipsList() => repository.GetAll().ToList();



        /// <summary>
        /// Возвращает список кораблей с ХП больше нуля (для игры).
        /// </summary>
        /// <returns>Список кораблей с ХП больше нуля.</returns>
        public List<Ship> GetShipsInBattleList()
        {
            List<Ship> ShipsInBattle = (from ship in repository.GetAll()
                                 where ship.Hp > 0
                                 select ship)
                                 .ToList();
                                 
            return ShipsInBattle;                
        }



        /// <summary>
        /// Восстанавливает ХП кораблям и определяет, кто ходит первым.
        /// </summary>
        public void InitializeGame()
        {
            GameOverNotify = null;

            if (GetShipsList().Count > 0)
            {
                ResetTurns();
                RecoverHP();
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
            {
                ((Ship)ship).Name = name;
                repository.Update((Ship)ship);
            }

            if (string.IsNullOrWhiteSpace(name) || (!string.IsNullOrWhiteSpace(name) && flagColor != "_No_Color_"))
            {
                SetFlagColor((Ship)ship, flagColor);
                repository.Update((Ship)ship);
            }
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
            ((Ship)ship).Hp -= 20;
            repository.Update((Ship)ship);

            PassTheTurn();
        }



        /// <summary>
        /// Возвращает ХП корабля после его лечения, соответственно меняет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>ХП корабля.</returns>
        public void HealShipHP(object ship)
        {
            ((Ship)ship).Hp += 15;
            repository.Update((Ship)ship);

            PassTheTurn();
        }



        /// <summary>
        /// Проверяет, остался ли в игре один единственный корабль. Вызывает событие завершения игры.
        /// </summary>
        public void CheckShipsInBattle()
        {
            if (GetShipsInBattleList().Count <= 1)
            {
                PassTheTurn();
                GameOverNotify?.Invoke();   
            }
        }



        /// <summary>
        /// Передает ход следующему кораблю (игроку), забирает ход у предыдущего.
        /// </summary>
        private void PassTheTurn()
        {
            Ship ship1;
            Ship ship2;

            if (GetShipsInBattleList().Last().IsYourTurn == true)
            {
                ship1 = GetShipsInBattleList().Last();
                ship1.IsYourTurn = false;
                repository.Update(ship1);

                ship2 = GetShipsInBattleList()[0];
                ship2.IsYourTurn = true;
                repository.Update(ship2);

                return;
            }

            for(int i = 0; i < GetShipsInBattleList().Count; i++) 
            {
                if (GetShipsInBattleList()[i].IsYourTurn == true)
                {
                    ship1 = GetShipsInBattleList()[i];
                    ship2 = GetShipsInBattleList()[i + 1];

                    ship1.IsYourTurn = false;
                    repository.Update(ship1);

                    ship2.IsYourTurn = true;
                    repository.Update(ship2);

                    return;
                }
            }

            ship1 = GetShipsInBattleList()[0];
            ship1.IsYourTurn = true;
            repository.Update(ship1);
        }



        /// <summary>
        /// Возвращает корабль, который сейчас ходит.
        /// </summary>
        /// <returns>Объект корабля. Null, если корабль не найден.</returns>
        public Ship GetTurnShip()
        {
            foreach (Ship ship in GetShipsInBattleList()) 
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
        public void RecoverHP()
        {
            foreach (Ship ship in GetShipsList())
            {
                ship.Hp = 100;
                repository.Update(ship);     
            }
        }



        /// <summary>
        /// Сбрасывает, кто сейчас ходит.
        /// </summary>
        public void ResetTurns()
        {
            foreach (Ship ship in GetShipsList())
            {
                ship.IsYourTurn = false;
                repository.Update(ship);
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
