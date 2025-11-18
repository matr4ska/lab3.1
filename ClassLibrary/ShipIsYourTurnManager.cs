using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ShipIsYourTurnManager
    {
        public ShipIsYourTurnManager(ShipManager shipManager)
        {
            this.shipManager = shipManager;
        }

        private ShipManager shipManager;



        /// <summary>
        /// Определяет, какой корабль сейчас ходит.
        /// </summary>
        public void PassTheTurn()
        {
            Ship ship1;
            Ship ship2;

            if (shipManager.GetNotDeadShipsList().Last().IsYourTurn == true)
            {
                ship1 = shipManager.GetNotDeadShipsList().Last();
                ship2 = shipManager.GetNotDeadShipsList()[0];

                shipManager.ChangeIsYourTurn(ship1, false);
                shipManager.ChangeIsYourTurn(ship2, true);

                return;
            }

            for (int i = 0; i < shipManager.GetNotDeadShipsList().Count; i++)
            {
                if (shipManager.GetNotDeadShipsList()[i].IsYourTurn == true)
                {
                    ship1 = shipManager.GetNotDeadShipsList()[i];
                    ship2 = shipManager.GetNotDeadShipsList()[i + 1];

                    shipManager.ChangeIsYourTurn(ship1, false);
                    shipManager.ChangeIsYourTurn(ship2, true);

                    return;
                }
            }

            ship1 = shipManager.GetNotDeadShipsList()[0];
            shipManager.ChangeIsYourTurn(ship1, true);
        }



        /// <summary>
        /// Возвращает корабль, который сейчас ходит.
        /// </summary>
        /// <returns>Объект корабля</returns>
        public Ship GetTurnShip()
        {
            foreach (Ship ship in shipManager.GetNotDeadShipsList())
            {
                if (ship.IsYourTurn == true)
                {
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
            foreach (Ship ship in shipManager.GetShipsList())
            {
                shipManager.ChangeIsYourTurn(ship, false);
            }
        }
    }
}
