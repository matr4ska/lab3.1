using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ShipHPManager
    {
        public ShipHPManager(ShipManager shipManager)
        {
            this.shipManager = shipManager;
        }

        private ShipManager shipManager;



        /// <summary>
        /// Убавляет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        public void AttackShipHP(object ship)
        {
            shipManager.ChangeHP(ship, ((Ship)ship).Hp - 20);
        }



        /// <summary>
        /// Прибавляет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        public void HealShipHP(object ship)
        {
            shipManager.ChangeHP(ship, ((Ship)ship).Hp + 15);
        }



        /// <summary>
        /// Восстанавливает ХП всем кораблям.
        /// </summary>
        public void RecoverHP()
        {
            foreach (Ship ship in shipManager.GetShipsList())
            {
                shipManager.ChangeHP(ship, 100);
            }
        }
    }
}

