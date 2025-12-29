using Shared.ViewGame_EventArgs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.WinForms_Interfaces
{
    public interface IGameView
    {
        public event EventHandler<ViewGame_OnShipAttackedEventArgs> OnShipAttacked;
        public event EventHandler<ViewGame_OnShipHealedEventArgs> OnShipHealed;
        public event Action OnShipsInBattleListUpdated;
        public event Action OnNewBattleStarted;
        public event Action OnCheckIfGameIsOver;
        public event Action OnPassTheTurn;
        public event Action OnGetTurnShip;


        /// <summary>
        /// Метод, вызываемый в конце хода игрока
        /// </summary>
        /// <param name="selectedShipIndex">Индекс последнего выбранного игроком корабля</param>
        public void EndTheTurn(int selectedShipIndex);

        /// <summary>
        /// Уменьшает ХП кораблю
        /// </summary>
        /// <param name="id">ID корабля</param>
        public void AttackShip(string id);

        /// <summary>
        /// Увеличивает ХП кораблю
        /// </summary>
        /// <param name="id">ID корабля</param>
        public void HealShip(string id);

        /// <summary>
        /// Передает ход следующему игроку
        /// </summary>
        public void PassTheTurn();

        /// <summary>
        /// Выводит название корабля, который сейчас ходит (а эм вообще то корабли не могут ходить они плавают)
        /// </summary>
        public void SetTurnShip();

        /// <summary>
        /// Пробует закончить игру, если выполняются условия
        /// </summary>
        public void TryEndBattle();

        /// <summary>
        /// Пересоздает пространство представления списка кораблей с более актуальными данными
        /// </summary>
        /// <param name="shipsInBattle">Список списков (кораблей) строк (свойств корабля)</param>
        public void UpdateShipsInBattleList(List<List<string>> shipsInBattle);

        /// <summary>
        /// Меняет надпись с названием корабля, который сейчас ходит
        /// </summary>
        /// <param name="name">Новое название корабля, который сейчас ходит</param>
        public void ChangeTurnShipName(string name);

        /// <summary>
        /// Выводит сообщение о конце игры
        /// </summary>
        public void ShowGameOverMessage();

        /// <summary>
        /// Инициализирует новую игру
        /// </summary>
        public void StartNewBattle();

        /// <summary>
        /// Сбрасывает игру
        /// </summary>
        public void ResetGame();

        /// <summary>
        /// Закрывает окно игры
        /// </summary>
        public void CloseGameScreen();

        /// <summary>
        /// Инициализирует элементы формы. Использовать в презентере
        /// </summary>
        public void InitializeGameFormFirstTime();
    }
}
