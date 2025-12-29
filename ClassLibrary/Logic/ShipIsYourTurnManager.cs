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
    public class ShipIsYourTurnManager : IShipIsYourTurnManager
    {
        public event EventHandler<OnGetTurnShipEventArgs> OnGetTurnShip;

        public ShipIsYourTurnManager(IShipManager shipManager)
        {
            this.shipManager = shipManager;
        }

        private IShipManager shipManager;



        /// <summary>
        /// Определяет, какой корабль сейчас ходит. Управляет системой ходов
        /// </summary>
        public void PassTheTurn()
        {
            int ship1Id;
            int ship2Id;
            List<Ship> shipsInBattle = shipManager.GetShipsInBattleList();

            if (shipsInBattle.Last().IsYourTurn == true)
            {
                ship1Id = shipsInBattle.Last().Id;
                ship2Id = shipsInBattle[0].Id;

                shipManager.ChangeIsYourTurn(ship1Id, false);
                shipManager.ChangeIsYourTurn(ship2Id, true);

                return;
            }

            for (int i = 0; i < shipManager.GetShipsInBattleList().Count; i++)
            {
                if (shipsInBattle[i].IsYourTurn == true)
                {
                    ship1Id = shipsInBattle[i].Id;
                    ship2Id = shipsInBattle[i + 1].Id;

                    shipManager.ChangeIsYourTurn(ship1Id, false);
                    shipManager.ChangeIsYourTurn(ship2Id, true);

                    return;
                }
            }

            ship1Id = shipsInBattle[0].Id;
            shipManager.ChangeIsYourTurn(ship1Id, true);
        }



        /// <summary>
        /// Возвращает корабль, который сейчас ходит.
        /// </summary>
        /// <returns>Объект корабля</returns>
        public Ship GetTurnShip()
        {
            List<Ship> shipsInBattle = shipManager.GetShipsInBattleList();

            foreach (Ship ship in shipsInBattle)
            {
                if (ship.IsYourTurn == true)
                {
                    OnGetTurnShip(this, new OnGetTurnShipEventArgs(ship.Name));
                    return ship;
                }
            }

            return null;
        }



        /// <summary>
        /// Сбрасывает ходы.
        /// </summary>
        public void ResetTurns()
        {
            List<Ship> ships = shipManager.GetShipsList();

            foreach (Ship ship in ships)
            {
                shipManager.ChangeIsYourTurn(ship.Id, false);
            }
        }
    }
}
