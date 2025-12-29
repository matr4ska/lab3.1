using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewGame_EventArgs
{
    public class ViewGame_OnShipAttackedEventArgs : EventArgs
    {
        public int Id { get; set; }

        public ViewGame_OnShipAttackedEventArgs(string id)
        {
            Id = Convert.ToInt32(id);
        }
    }
}
