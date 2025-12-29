using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ModelEventArgs
{
    public class OnFlagColorNamesRequestedEventArgs : EventArgs
    {
        public List<string> FlagColorNames { get; set; }

        public OnFlagColorNamesRequestedEventArgs(List<string> flagColorNames)
        {
            FlagColorNames = flagColorNames;
        }
    }
}
