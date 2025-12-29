using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewMain_EventArgs
{
    public class ViewMain_OnShipNameChangedEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ViewMain_OnShipNameChangedEventArgs(string id, string name)
        {
            Id = Convert.ToInt32(id);
            Name = name;
        }
    }
}
