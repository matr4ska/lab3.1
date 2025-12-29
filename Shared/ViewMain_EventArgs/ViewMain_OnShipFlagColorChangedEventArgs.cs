using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewMain_EventArgs
{
    public class ViewMain_OnShipFlagColorChangedEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string FlagColor { get; set; }

        public ViewMain_OnShipFlagColorChangedEventArgs(string id, object flagColor)
        {
            Id = Convert.ToInt32(id);
            FlagColor = flagColor.ToString();
        }
    }
}
