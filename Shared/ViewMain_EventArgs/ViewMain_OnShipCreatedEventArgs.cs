using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewMain_EventArgs
{
    public class ViewMain_OnShipCreatedEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string FlagColor { get; set; }

        public ViewMain_OnShipCreatedEventArgs(string name, string flagColor)
        {
            Name = name;
            FlagColor = flagColor;
        }
    }
}
