using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ModelEventArgs
{
    public class OnShipsInBattleListUpdatedEventArgs : EventArgs
    {
        public List<List<string>> ShipsInBattle { get; set; }

        public OnShipsInBattleListUpdatedEventArgs(List<List<string>> shipsInBattle)
        {
            ShipsInBattle = shipsInBattle;
        }
    }
}
