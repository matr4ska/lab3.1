using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class BattleManager
    {
        public BattleManager(ShipManager shipManager, ShipHPManager shipHPManager, ShipIsYourTurnManager shipIsYourTurnManager)
        {
            this.shipManager = shipManager;
            this.shipIsYourTurnManager = shipIsYourTurnManager;
            this.shipHPManager = shipHPManager;
        }

        private ShipManager shipManager;
        private ShipIsYourTurnManager shipIsYourTurnManager;
        private ShipHPManager shipHPManager;


        public delegate void GameOverHandler();
        public event GameOverHandler? BattleIsOverNotify;



        /// <summary>
        /// Сбрасывает все параметры прошлой игры для новой игры.
        /// </summary>
        public void InitializeNewBattle()
        {
            BattleIsOverNotify = null;

            shipIsYourTurnManager.ResetTurns();
            shipHPManager.RecoverHP();
            shipIsYourTurnManager.PassTheTurn();
        }



        /// <summary>
        /// Проверяет, остался ли в игре один единственный корабль. Если True - вызывает событие завершения игры.
        /// </summary>
        public void CheckIfBattleIsOver()
        {
            if (shipManager.GetNotDeadShipsList().Count <= 1)
            {
                BattleIsOverNotify?.Invoke();
            }
        }
    }
}
