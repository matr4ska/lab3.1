using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewGame_EventArgs
{
    public class ViewGame_OnShipHealedEventArgs : EventArgs
    {
        public int Id { get; set; }

        public ViewGame_OnShipHealedEventArgs(string id)
        {
            Id = Convert.ToInt32(id);
        }
    }
}
