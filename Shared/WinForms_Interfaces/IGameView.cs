using Shared.ViewGame_EventArgs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.WinForms_Interfaces
{
    public interface IGameView
    {
        public event EventHandler<ViewGame_OnShipAttackedEventArgs> OnShipAttacked;
        public event EventHandler<ViewGame_OnShipHealedEventArgs> OnShipHealed;
        public event Action OnShipsInBattleListUpdated;
        public event Action OnNewBattleStarted;
        public event Action OnCheckIfGameIsOver;
        public event Action OnPassTheTurn;
        public event Action OnGetTurnShip;


        public void EndTheTurn(int selectedShipIndex);

        public void AttackShip(string id);

        public void HealShip(string id);

        public void PassTheTurn();

        public void UpdateShipsInBattle();

        public void SetTurnShip();

        public void TryEndBattle();

        public void UpdateShipsInBattleList(List<List<string>> shipsInBattle);

        public void ChangeTurnShipName(string name);

        public void ShowGameOverMessage();

        public void StartNewBattle();

        public void ResetGame();

        public void CloseGameScreen();

        public void InitializeGameFormFirstTime();
    }
}
