using Shared.ViewMain_EventArgs;
using System.Drawing;

namespace Shared.WinForms_Interfaces
{
    public interface IMainView
    {
        public event EventHandler<ViewMain_OnShipCreatedEventArgs> OnShipCreated;
        public event EventHandler<ViewMain_OnShipDeletedEventArgs> OnShipDeleted;
        public event EventHandler<ViewMain_OnShipNameChangedEventArgs> OnShipNameChanged;
        public event EventHandler<ViewMain_OnShipFlagColorChangedEventArgs> OnShipFlagColorChanged;
        public event Action OnShipListUpdated;
        public event Action OnHelpTextRequested;
        public event Action OnFlagColorNamesRequested;

 
        public void InitializeShipList();

        public void InitializeFlagColorOptions(List<string> flagColorNames);

        public void InitializeMainFormFirstTime();

        public void CreateShip(string name, string flagColor);

        public void DeleteShip(string id);

        public void ChangeShipName(string id, string name);

        public void ChangeShipFlagColor(string id, string flagColor);

        public void UpdateShipList(List<List<string>> shipsProperties);

        public void StartGame();

        public void ShowHelp(string helpText);
    }
}
