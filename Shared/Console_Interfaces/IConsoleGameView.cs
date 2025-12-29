using Shared.ViewGame_EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Console_Interfaces
{
    public interface IConsoleGameView
    {
        public event EventHandler<ViewGame_OnShipAttackedEventArgs> OnShipAttacked;
        public event EventHandler<ViewGame_OnShipHealedEventArgs> OnShipHealed;
        public event Action OnShipsInBattleListUpdated;
        public event Action OnNewBattleStarted;
        public event Action OnPassTheTurn;
        public event Action OnGetTurnShip;

        /// <summary>
        /// Выводит список кораблей с ХП больше нуля
        /// </summary>
        public void ShowShipsInBattle();

        /// <summary>
        /// Пересоздает пространство представления списка кораблей с более актуальными данными
        /// </summary>
        /// <param name="shipsInBattle">Список списков (кораблей) строк (свойств корабля)</param>
        public void UpdateShipsInBattle(List<List<string>> shipsProperties);

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
        /// Меняет надпись с названием корабля, который сейчас ходит
        /// </summary>
        /// <param name="name">Новое название корабля, который сейчас ходит</param>
        public void ChangeTurnShipName(string name);

        /// <summary>
        /// Инициализирует новую игру
        /// </summary>
        public void StartNewBattle();

        /// <summary>
        /// Сбрасывает игру
        /// </summary>
        public void ResetGame();
    }
}
