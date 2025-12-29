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
    public class ShipHPManager : IShipHPManager
    {
        public ShipHPManager(IShipManager shipManager)
        {
            this.shipManager = shipManager;
        }

        private IShipManager shipManager;



        public void AttackShipHP(int id)
        {
            shipManager.ChangeHPByValue(id, -20);
        }



        public void HealShipHP(int id)
        {
            shipManager.ChangeHPByValue(id, +15);
        }



        public void RecoverHP()
        {
            List<Ship> ships = shipManager.GetShipsList();
            foreach (Ship ship in ships)
            {
                shipManager.ChangeHPByValue(ship.Id, 100 - ship.Hp);
            }
        }
    }
}

