using ClassLibrary.ModelEventArgs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IShipIsYourTurnManager
    {
        public event EventHandler<OnGetTurnShipEventArgs> OnGetTurnShip;

        /// <summary>
        /// Определяет, какой корабль сейчас ходит. Управляет системой ходов
        /// </summary>
        public void PassTheTurn();

        /// <summary>
        /// Возвращает корабль, который сейчас ходит
        /// </summary>
        /// <returns>Объект корабля</returns>
        public Ship GetTurnShip();

        /// <summary>
        /// Сбрасывает ходы
        /// </summary>
        public void ResetTurns();
    }
}
