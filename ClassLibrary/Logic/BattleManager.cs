using ClassLibrary.Interfaces;
using ClassLibrary.ModelEventArgs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Logic
{
    public class BattleManager : IBattleManager
    {
        public event Action GameOverNotification;

        public BattleManager(IShipManager shipManager, IShipHPManager shipHPManager, IShipIsYourTurnManager shipIsYourTurnManager)
        {
            this.shipManager = shipManager;
            this.shipIsYourTurnManager = shipIsYourTurnManager;
            this.shipHPManager = shipHPManager;
        }

        private IShipManager shipManager;
        private IShipIsYourTurnManager shipIsYourTurnManager;
        private IShipHPManager shipHPManager;

        

        public void InitializeNewBattle()
        {
            shipIsYourTurnManager.ResetTurns();
            shipHPManager.RecoverHP();
            shipIsYourTurnManager.PassTheTurn();
        }



        public void CheckIfBattleIsOver()
        {
            if (shipManager.GetShipsInBattleList().Count <= 1)
            {
                GameOverNotification();
            }
        }
    }
}
