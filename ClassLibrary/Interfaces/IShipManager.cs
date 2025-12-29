using ClassLibrary.ModelEventArgs;
using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IShipManager
    {
        public event EventHandler<OnShipListUpdatedEventArgs> OnShipListUpdated;
        public event EventHandler<OnShipsInBattleListUpdatedEventArgs> OnShipsInBattleListUpdated;

        /// <summary>
        /// Создает объект корабля, добавляет его в общий список кораблей.
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="flagColor">Цвет флага</param>
        public void CreateShip(string name, string flagColor);

        /// <summary>
        /// Удаляет корабль
        /// </summary>
        /// <param name="id">ID корабля</param>
        public void DeleteShip(int id);

        /// <summary>
        /// Возвращает корабль
        /// </summary>
        /// <param name="id">ID корабля</param>
        /// <returns>Объект корабля</returns>
        public Ship GetShip(int id);

        /// <summary>
        /// Возвращает список всех кораблей
        /// </summary>
        /// <returns>Список всех кораблей</returns>
        public List<Ship> GetShipsList();

        /// <summary>
        /// Возвращает список всех свойства каждого корабля. Метод для передачи во View.
        /// </summary>
        public void GetShipsListInView();

        /// <summary>
        /// Возвращает список всех кораблей с ХП больше нуля
        /// </summary>
        /// <returns>Cписок кораблей с ХП больше нуля</returns>
        public List<Ship> GetShipsInBattleList();

        /// <summary>
        /// Возвращает список всех кораблей с ХП больше нуля. Для передачи во View
        /// </summary>
        public void GetShipsInBattleListInView();

        /// <summary>
        /// Меняет название кораблю
        /// </summary>
        /// <param name="id">ID корабля</param>
        /// <param name="name">Новое название корабля</param>
        public void ChangeShipName(int id, string name);

        /// <summary>
        /// Меняет цвет флага кораблю
        /// </summary>
        /// <param name="id">ID корабля</param>
        /// <param name="flagColor">Название нового цвета флага корабля</param>
        public void ChangeFlagColor(int id, string flagColor);

        /// <summary>
        /// Меняет ХП кораблю на заданное значение
        /// </summary>
        /// <param name="id">ID корабля</param>
        /// <param name="addHp">Прибавляемое кораблю ХП</param>
        public void ChangeHPByValue(int id, int addHp);

        /// <summary>
        /// Меняет, ходит или не ходит сейчас корабль
        /// </summary>
        /// <param name="id">ID корабля</param>
        /// <param name="isYourTurn">Ходит ли сейчас корабль</param>
        public void ChangeIsYourTurn(int id, bool isYourTurn);
    }
}
