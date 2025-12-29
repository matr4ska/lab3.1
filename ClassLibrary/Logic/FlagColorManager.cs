using ClassLibrary.Interfaces;
using ClassLibrary.ModelEventArgs;
using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Logic
{
    public class FlagColorManager : IFlagColorManager
    {
        public event EventHandler<OnFlagColorNamesRequestedEventArgs> OnFlagColorNamesRequested;


        public List<string> GetFlagColorNames()
        {
            List<string> flagColorNames = Enum.GetNames(typeof(FlagColor)).ToList();
            OnFlagColorNamesRequested(this, new OnFlagColorNamesRequestedEventArgs(flagColorNames));
            return flagColorNames;
        }



        public FlagColor ConvertFlagColorFromString(string color)
        {
            switch (color)
            {
                case "Red": return FlagColor.Red;
                case "Green": return FlagColor.Green;
                case "Blue": return FlagColor.Blue;
                case "Yellow": return FlagColor.Yellow;
                case "Pink": return FlagColor.Pink;
                case "Black": return FlagColor.Black;

                default: return FlagColor._No_Color_;
            }
        }
    }
}
