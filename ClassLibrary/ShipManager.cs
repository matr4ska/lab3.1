using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ShipManager
    {
        public ShipManager(IRepository<Ship> repository)
        {
            this.repository = repository;
        }

        public IRepository<Ship> repository;



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
                    FlagColor = FlagColorManager.ConvertFlagColorFromString(flagColor.ToString()),
                    IsYourTurn = false,
                };
                
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
        public List<Ship> GetNotDeadShipsList()
        {
            List<Ship> ShipsInBattle = (from ship in repository.GetAll()
                                        where ship.Hp > 0
                                        select ship).ToList();

            return ShipsInBattle;
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
        public void ChangeFlagColor(object ship, string flagColor)
        {
            if (flagColor != "_No_Color_")
            {
                ((Ship)ship).FlagColor = FlagColorManager.ConvertFlagColorFromString(flagColor);
                repository.Update((Ship)ship);
            }
        }



        /// <summary>
        /// Меняет ХП кораблю.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <param name="hp">Новое ХП корабля</param>
        public void ChangeHP(object ship, int hp)
        {
            ((Ship)ship).Hp = hp;
            repository.Update((Ship)ship);
        }



        /// <summary>
        /// Меняет ходит или не ходит сейчас корабль.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <param name="isYourTurn">Ходит ли сейчас корабль</param>
        public void ChangeIsYourTurn(object ship, bool isYourTurn)
        {
            ((Ship)ship).IsYourTurn = isYourTurn;
            repository.Update((Ship)ship);
        }
    }
}
