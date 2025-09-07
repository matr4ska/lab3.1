using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Ship : PropertyChangedNotification
    {
        public Ship(string name, sbyte hp, FlagColor color) 
        { 
            Name = name;
            Hp = hp;
            Color = color;
        }

        private string name;
        private sbyte hp;
        private FlagColor color;


        public string Name
        {
            get => name;
            set  
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                    OnPropertyChanged();
                }
                else
                    name = string.Empty;   
            }
        }


        public sbyte Hp
        {
            get => hp;
            set
            {
                hp = value; 
                OnPropertyChanged();
            }
        }


        public FlagColor Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }
    }

 
}
