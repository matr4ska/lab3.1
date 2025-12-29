using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ModelEventArgs
{
    public class OnGetTurnShipEventArgs : EventArgs
    {
        public string Name { get; set; }

        public OnGetTurnShipEventArgs(string name)
        {
            Name = name;
        }
    }
}
