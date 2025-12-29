using Shared.ViewGame_EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Console_Interfaces
{
    public interface IConsoleGameView
    {
        public event EventHandler<ViewGame_OnShipAttackedEventArgs> OnShipAttacked;
        public event EventHandler<ViewGame_OnShipHealedEventArgs> OnShipHealed;
        public event Action OnShipsInBattleListUpdated;
        public event Action OnNewBattleStarted;
        public event Action OnPassTheTurn;
        public event Action OnGetTurnShip;


        public void ShowShipsInBattle();

        public void UpdateShipsInBattle(List<List<string>> shipsProperties);

        public void AttackShip(string id);

        public void HealShip(string id);

        public void PassTheTurn();

        public void SetTurnShip();

        public void ChangeTurnShipName(string name);

        public void StartNewBattle();

        public void ResetGame();
    }
}
