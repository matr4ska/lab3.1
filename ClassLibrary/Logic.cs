using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary
{
    public class Logic
    {
        public List<Ship> Ships { get; set; } = new List<Ship>
        {
            new Ship("Vista", FlagColor.Green),
            new Ship("Kingslayer", FlagColor.Red),
            new Ship("ObraDinn", FlagColor.Blue)
        };


        public Ship CreateShip(string name, object flagColor)
        {
            Ship ship = new Ship(name, (FlagColor)flagColor);
            Ships.Add(ship);
            return ship;
        }


        public void DeleteShip(object ship) => Ships.Remove((Ship)ship);


        public Ship GetShip(object ship) => Ships[Ships.IndexOf((Ship)ship)];


        public void ChangeShipAttributes(object ship, string name) => Ships[Ships.IndexOf((Ship)ship)].Name = name;


        public void ChangeShipAttributes(object ship, object flagColor) => Ships[Ships.IndexOf((Ship)ship)].FlagColor = (FlagColor)flagColor;


        public short AttackShip(object ship)
        {
            Ships[Ships.IndexOf((Ship)ship)].Hp -= 20;
            return Ships[Ships.IndexOf((Ship)ship)].Hp;
        }


        public short HealShip(object ship)
        {
            Ships[Ships.IndexOf((Ship)ship)].Hp += 15;
            return Ships[Ships.IndexOf((Ship)ship)].Hp;
        }


        public Ship PassTheTurn()
        {
            var unbeatenShips = (from ship in Ships
                                 where ship.Hp > 0
                                 select ship)
                                 .ToList();

            if (unbeatenShips.Last().IsYourTurn == true)
            {
                unbeatenShips.Last().IsYourTurn = false;
                unbeatenShips[0].IsYourTurn = true;
                return unbeatenShips[0];
            }

            foreach (Ship ship in unbeatenShips)
            {
                if (ship.IsYourTurn == true)
                {
                    ship.IsYourTurn = false;
                    unbeatenShips[unbeatenShips.IndexOf(ship) + 1].IsYourTurn = true;
                    return unbeatenShips[unbeatenShips.IndexOf(ship) + 1];
                }
            }

            unbeatenShips[0].IsYourTurn = true;
            return unbeatenShips[0];
        }


        public bool CheckShipBeaten(object ship)
        {
            if (Ships[Ships.IndexOf((Ship)ship)].Hp <= 0)
            {
                return true;
            }

            return false;
        }


        public bool CheckGameOver()
        {
            var unbeatenShips = (from ship in Ships
                                where ship.Hp > 0
                                select ship)
                                .Count();

            if (unbeatenShips == 1)
                return true;

            return false; 
        }

        
        public void RecoverHP()
        {
            foreach (Ship ship in Ships)
                ship.Hp = 100;
        }
    }
}
