using ClassLibrary.Interfaces;
using ClassLibrary.ModelEventArgs;
using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary.Logic
{
    public class ShipManager : IShipManager
    {
        public event EventHandler<OnShipListUpdatedEventArgs> OnShipListUpdated;
        public event EventHandler<OnShipsInBattleListUpdatedEventArgs> OnShipsInBattleListUpdated;


        public ShipManager(IRepository<Ship> repository, IFlagColorManager flagColorManager)
        {
            this.repository = repository;
            this.flagColorManager = flagColorManager;
        }

        public IRepository<Ship> repository;
        public IFlagColorManager flagColorManager;



        public void CreateShip(string name, string flagColor)
        {
            if (!string.IsNullOrWhiteSpace(name) && flagColor.ToString() != "_No_Color_")
            {
                Ship ship = new Ship()
                {
                    Name = name,
                    Hp = 100,
                    FlagColor = flagColorManager.ConvertFlagColorFromString(flagColor.ToString()),
                    IsYourTurn = false,
                };

                repository.Create(ship);
            }
        }



        public void DeleteShip(int id)
        {
            repository.Delete(id);
        }



        public Ship GetShip(int id)
        {
            return repository.GetItem(id);
        }



        public void GetShipsListInView()
        {
            List<List<string>> shipsProperties = new List<List<string>>();
            List<Ship> ships = repository.GetAll().ToList();

            for (int i = 0; i < ships.Count(); i++)
            {
                shipsProperties.Add(new List<string>());      
                shipsProperties[i].Add(ships[i].Hp.ToString());
                shipsProperties[i].Add(ships[i].Name.ToString());
                shipsProperties[i].Add(ships[i].FlagColor.ToString());
                shipsProperties[i].Add(ships[i].Id.ToString());
            }

            OnShipListUpdated(this, new OnShipListUpdatedEventArgs(shipsProperties));
        }



        public List<Ship> GetShipsList()
        {
            return repository.GetAll().ToList();
        }



        public void GetShipsInBattleListInView()
        {
            List<Ship> ShipsInBattle = (from ship in repository.GetAll()
                                        where ship.Hp > 0
                                        select ship).ToList();

            List<List<string>> shipsInBattle = new List<List<string>>();

            for (int i = 0; i < ShipsInBattle.Count(); i++)
            {
                shipsInBattle.Add(new List<string>());
                shipsInBattle[i].Add(ShipsInBattle[i].Hp.ToString());
                shipsInBattle[i].Add(ShipsInBattle[i].Name.ToString());
                shipsInBattle[i].Add(ShipsInBattle[i].FlagColor.ToString());
                shipsInBattle[i].Add(ShipsInBattle[i].Id.ToString());
            }

            OnShipsInBattleListUpdated(this, new OnShipsInBattleListUpdatedEventArgs(shipsInBattle));
        }



        public List<Ship> GetShipsInBattleList()
        {
            return (from ship in repository.GetAll()
                    where ship.Hp > 0
                    select ship).ToList();
        }



        public void ChangeShipName(int id, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Ship ship = GetShip(id);
                ship.Name = name;
                repository.Update(ship);
            }
        }



        public void ChangeFlagColor(int id, string flagColor)
        {
            if (flagColor != "_No_Color_")
            {
                Ship ship = GetShip(id);
                ship.FlagColor = flagColorManager.ConvertFlagColorFromString(flagColor);
                repository.Update(ship);
            }
        }



        public void ChangeHPByValue(int id, int addHp)
        {
            Ship ship = GetShip(id);
            ship.Hp += addHp;
            repository.Update(ship);
        }



        public void ChangeIsYourTurn(int id, bool isYourTurn)
        {
            Ship ship = GetShip(id);
            ship.IsYourTurn = isYourTurn;
            repository.Update(ship);
        }
    }
}
