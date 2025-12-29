using Shared.ViewGame_EventArgs;
using Shared.ViewMain_EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Console_Interfaces
{
    public interface IConsoleMainView
    {
        public event EventHandler<ViewMain_OnShipCreatedEventArgs> OnShipCreated;
        public event EventHandler<ViewMain_OnShipDeletedEventArgs> OnShipDeleted;
        public event EventHandler<ViewMain_OnShipNameChangedEventArgs> OnShipNameChanged;
        public event EventHandler<ViewMain_OnShipFlagColorChangedEventArgs> OnShipFlagColorChanged;
        public event Action OnShipListUpdated;
        public event Action OnFlagColorNamesRequested;


        public void InitializeFlagColorOptions(List<string> flagColorNames);

        public void CreateShip(string name, string flagColor);

        public void DeleteShip(string id);

        public void ChangeShipName(string id, string name);

        public void ChangeShipFlagColor(string id, string flagColor);

        public void UpdateShipList(List<List<string>> shipsProperties);

        public void ShowShipList(); 
    }
}
