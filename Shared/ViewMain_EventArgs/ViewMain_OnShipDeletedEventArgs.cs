using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewMain_EventArgs
{
    public class ViewMain_OnShipDeletedEventArgs : EventArgs
    {
        public int Id { get; set; }

        public ViewMain_OnShipDeletedEventArgs(string id)
        {
            Id = Convert.ToInt32(id);
        }
    }
}
