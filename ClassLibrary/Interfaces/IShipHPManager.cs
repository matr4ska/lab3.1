using ClassLibrary.ModelEventArgs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IShipHPManager
    {
        /// <summary>
        /// Убавляет кораблю ХП
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        public void AttackShipHP(int id);

        /// <summary>
        /// Прибавляет кораблю ХП
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        public void HealShipHP(int id);

        /// <summary>
        /// Восстанавливает ХП всем кораблям
        /// </summary>
        public void RecoverHP();
    }
}
