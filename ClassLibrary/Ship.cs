using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Ship
    {
        public Ship(string name, FlagColor flagColor) 
        { 
            Name = name;
            Hp = 100;
            FlagColor = flagColor;
            IsYourTurn = false;
        }


        private string name;
        private short hp;
        private FlagColor flagColor;
        private bool isYourTurn;
        private bool isBeaten;


        public string Name
        {
            get => name;
            set => name = value; 
        }

        public short Hp
        {
            get => hp;
            set => hp = value; 
        }

        public FlagColor FlagColor
        {
            get => flagColor;
            set => flagColor = value;
        }

        public bool IsYourTurn
        {
            get => isYourTurn;
            set => isYourTurn = value;
        }

        public bool IsBeaten
        {
            get => isBeaten;
            set => isBeaten = value;
        }
    }
}
