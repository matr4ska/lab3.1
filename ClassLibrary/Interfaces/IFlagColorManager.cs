using ClassLibrary.ModelEventArgs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IFlagColorManager
    {
        public event EventHandler<OnFlagColorNamesRequestedEventArgs> OnFlagColorNamesRequested;

        /// <summary>
        /// Возвращает список названий всех цветов флагов.
        /// </summary>
        /// <returns>Список названий всех цветов флагов</returns>
        public abstract List<string> GetFlagColorNames();

        /// <summary>
        /// Возвращает FlagColor по названию.
        /// </summary>
        /// <param name="color">Название цвета</param>
        public abstract FlagColor ConvertFlagColorFromString(string color);
    }
}
