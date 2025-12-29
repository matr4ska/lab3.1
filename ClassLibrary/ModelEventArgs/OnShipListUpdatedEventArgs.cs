using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ModelEventArgs
{
    public class OnShipListUpdatedEventArgs : EventArgs
    {
        public List<List<string>> Ships { get; set; }

        public OnShipListUpdatedEventArgs(List<List<string>> ships)
        {
            Ships = ships;
        }
    }
}
