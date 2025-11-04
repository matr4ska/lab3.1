using DataAccessLayer;
using Model;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ClassLibrary
{
    public class Logic
    {
        public Logic()
        {
            repository = new EntityRepository<Ship>();
        }

        private IRepository<Ship> repository;

        public delegate void GameOverHandler();
        public event GameOverHandler? GameOverNotify;


        /// <summary>
        /// Создает объект корабля, добавляет его в общий список кораблей.
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="flagColor">Цвет</param>
        /// <returns>Объект корабля. Null, если входные данные некорректны</returns>
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
        /// <param name="ship">Объект корабля</param>
        public void DeleteShip(object ship)
        {
            Ship _ship = (Ship)ship;
            repository.Delete(_ship.Id);
        }



        /// <summary>
        /// Возвращает объект корабля.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Объект искомого корабля. Null, если список кораблей пуст</returns>
        public Ship GetShip(object ship)
        {
            return repository.GetItem(((Ship)ship).Id);
        }



        /// <summary>
        /// Возвращает список всех кораблей.
        /// </summary>
        /// <returns>Список всех кораблей</returns>
        public List<Ship> GetShipsList() => repository.GetAll().ToList();



        /// <summary>
        /// Возвращает список кораблей с ХП больше нуля.
        /// </summary>
        /// <returns>Список кораблей с ХП больше нуля</returns>
        public List<Ship> GetShipsInBattleList()
        {
            List<Ship> ShipsInBattle = (from ship in repository.GetAll()
                                 where ship.Hp > 0
                                 select ship)
                                 .ToList();
                                 
            return ShipsInBattle;                
        }



        /// <summary>
        /// Делает все нужное для старта новой игры.
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
        /// Меняет название корабля.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <param name="name">Новое название корабля</param>
        public void ChangeShipName(object ship, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                ((Ship)ship).Name = name;
                repository.Update((Ship)ship);
            }
        }



        /// <summary>
        /// Меняет цвет флага корабля.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <param name="flagColor">Название нового цвета флага корабля</param>
        public void ChangeShipFlagColor(object ship, string flagColor)
        {
            if (flagColor != "_No_Color_")
            {
                SetFlagColor((Ship)ship, flagColor);
                repository.Update((Ship)ship);
            }
        }



        /// <summary>
        /// Возвращает список названий всех цветов флага.
        /// </summary>
        /// <returns>Список строковых названий всех цветов флага</returns>
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
        /// Убавляет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        public void AttackShipHP(object ship)
        {
            ((Ship)ship).Hp -= 20;            
            repository.Update((Ship)ship);        
        }



        /// <summary>
        /// Прибавляет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        public void HealShipHP(object ship)
        {
            ((Ship)ship).Hp += 10;
            repository.Update((Ship)ship);  
        }



        /// <summary>
        /// Проверяет, остался ли в игре один единственный корабль. Если True - вызывает событие завершения игры.
        /// </summary>
        public void CheckIfGameIsOver()
        {
            if (GetShipsInBattleList().Count <= 1)
            {
                GameOverNotify?.Invoke();
            }
        }



        /// <summary>
        /// Определяет, какого корабля сейчас ход.
        /// </summary>
        public void PassTheTurn()
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
        /// Выводит текст о том, как пользоваться приложением.
        /// </summary>
        /// <returns>Текст с помощью.</returns>
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
