using ClassLibrary.ModelEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IBattleManager
    {
        public event Action GameOverNotification;

        /// <summary>
        /// Сбрасывает все параметры прошлой битвы для новой
        /// </summary>
        public void InitializeNewBattle();

        /// <summary>
        /// Проверяет, остался ли в игре один единственный корабль. Если True - вызывает событие завершения игры
        /// </summary>
        public void CheckIfBattleIsOver();
    }
}
