using ClassLibrary.Interfaces;
using ClassLibrary.ModelEventArgs;
using Shared.ViewGame_EventArgs;
using Shared.WinForms_Interfaces;
using Shared.Console_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.ConsolePresenter
{
    public class GameConsolePresenter
    {
        IShipManager shipManager;
        IShipHPManager shipHPManager;
        IShipIsYourTurnManager shipIsYourTurnManager;
        IBattleManager battleManager;

        IConsoleGameView gameView;


        public GameConsolePresenter(IConsoleGameView gameView, IShipManager shipManager, IShipHPManager shipHPManager,
            IShipIsYourTurnManager shipIsYourTurnManager, IBattleManager battleManager)
        {
            this.gameView = gameView;
            this.shipManager = shipManager;
            this.shipHPManager = shipHPManager;
            this.shipIsYourTurnManager = shipIsYourTurnManager;
            this.battleManager = battleManager;


            gameView.OnShipsInBattleListUpdated += inModelUpdateShipsInBattleList;
            shipManager.OnShipsInBattleListUpdated += inViewUpdateShipsInBattleList;

            gameView.OnNewBattleStarted += inModelInitializeNewBattle;

            battleManager.GameOverNotification += gameView.ResetGame;

            gameView.OnShipAttacked += inModelAttackShip;

            gameView.OnShipHealed += inModelHealShip;

            gameView.OnPassTheTurn += inModelPassTurnShip;

            gameView.OnGetTurnShip += inModelGetTurnShip;
            shipIsYourTurnManager.OnGetTurnShip += inViewGetTurnShip;
        }


        /// <summary>
        /// Передает представлению список кораблей с ХП больше нуля
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inViewUpdateShipsInBattleList(object sender, OnShipsInBattleListUpdatedEventArgs e)
        {
            gameView.UpdateShipsInBattle(e.ShipsInBattle);
        }

        /// <summary>
        /// Запрашивает у модели список кораблей с ХП больше нуля
        /// </summary>
        public void inModelUpdateShipsInBattleList()
        {
            shipManager.GetShipsInBattleListInView();
        }

        /// <summary>
        /// Просит у модели инициализировать новую игру
        /// </summary>
        public void inModelInitializeNewBattle()
        {
            battleManager.InitializeNewBattle();
        }

        /// <summary>
        /// Просит у модели уменьшить ХП кораблю
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inModelAttackShip(object sender, ViewGame_OnShipAttackedEventArgs e)
        {
            shipHPManager.AttackShipHP(e.Id);
        }

        /// <summary>
        /// Просит у модели увеличить ХП кораблю
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inModelHealShip(object sender, ViewGame_OnShipHealedEventArgs e)
        {
            shipHPManager.HealShipHP(e.Id);
        }

        /// <summary>
        /// Просит у модели передать ход другому кораблю
        /// </summary>
        public void inModelPassTurnShip()
        {
            shipIsYourTurnManager.PassTheTurn();
        }

        /// <summary>
        /// Задает в представлении имя корабля, который сейчас ходит
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inViewGetTurnShip(object sender, OnGetTurnShipEventArgs e)
        {
            gameView.ChangeTurnShipName(e.Name);
        }

        /// <summary>
        /// Просит у модели имя корабля, который сейчас ходит
        /// </summary>
        public void inModelGetTurnShip()
        {
            shipIsYourTurnManager.GetTurnShip();
        }
    }
}


