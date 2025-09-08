using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Logic : PropertyChangedNotification
    {
        public ObservableCollection<Ship> Ships { get; set; } = new ObservableCollection<Ship>
        {
            new Ship("Vista", 100, FlagColor.Green),
            new Ship("Kingslayer", 100, FlagColor.Red),
            new Ship("ObraDinn", 100, FlagColor.Blue)
        };

        

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }

       



        public Ship CreateShip(string name, FlagColor color)
        {
            return new Ship(name, 100, color);
        }



        public void DeleteShip()
        {
            if (SelectedIndex != -1)
            {
                Ship ship = Ships[SelectedIndex];
                Ships.Remove(ship);
            }
            SelectedIndex = -1;
        }



        public Ship GetShip()
        {
            if (SelectedIndex != -1)
            {
                return Ships[SelectedIndex];
            }
            SelectedIndex = -1;
            return null;
        }



        public void ChangeShipAttributes(string name, FlagColor color)
        {
            if (SelectedIndex != -1)
            {
                Ship ship = Ships[SelectedIndex];
                ship.Name = name;
                ship.Color = color;
                Ships[SelectedIndex] = ship;
            }
            SelectedIndex = -1;
        }


    }
}
