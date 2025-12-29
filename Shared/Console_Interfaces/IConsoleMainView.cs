using Shared.ViewGame_EventArgs;
using Shared.ViewMain_EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Console_Interfaces
{
    public interface IConsoleMainView
    {
        public event EventHandler<ViewMain_OnShipCreatedEventArgs> OnShipCreated;
        public event EventHandler<ViewMain_OnShipDeletedEventArgs> OnShipDeleted;
        public event EventHandler<ViewMain_OnShipNameChangedEventArgs> OnShipNameChanged;
        public event EventHandler<ViewMain_OnShipFlagColorChangedEventArgs> OnShipFlagColorChanged;
        public event Action OnShipListUpdated;
        public event Action OnFlagColorNamesRequested;

        /// <summary>
        /// Задает пространство для выбора цвета флага кораблю
        /// </summary>
        /// <param name="flagColorNames">Список названий флагов</param>
        public void InitializeFlagColorOptions(List<string> flagColorNames);

        /// <summary>
        /// Создает корабль
        /// </summary>
        /// <param name="name">Название корабля</param>
        /// <param name="flagColor">Цвет флага</param>
        public void CreateShip(string name, string flagColor);

        /// <summary>
        /// Удаляет корабль
        /// </summary>
        /// <param name="id">ID корабля</param>
        public void DeleteShip(string id);

        /// <summary>
        /// Изменяет название кораблю
        /// </summary>
        /// <param name="id">ID корабля</param>
        /// <param name="name">Название корабля</param>
        public void ChangeShipName(string id, string name);

        /// <summary>
        /// Изменяет цвет флага кораблю
        /// </summary>
        /// <param name="id">ID корабля</param>
        /// <param name="flagColor">Цвет флага</param>
        public void ChangeShipFlagColor(string id, string flagColor);

        /// <summary>
        /// Пересоздает пространство для отображения списка кораблей с более актуальными данными
        /// </summary>
        /// <param name="shipsProperties">Список списков (кораблей) строк (свойств кораблей)</param>
        public void UpdateShipList(List<List<string>> shipsProperties);

        /// <summary>
        /// Выводит список всех кораблей
        /// </summary>
        public void ShowShipList(); 
    }
}
