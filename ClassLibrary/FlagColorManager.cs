using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class FlagColorManager
    {
        /// <summary>
        /// Возвращает список названий всех цветов флагов.
        /// </summary>
        /// <returns>Список названий всех цветов флагов</returns>
        public static List<string> GetFlagColorNames() => Enum.GetNames(typeof(FlagColor)).ToList();



        /// <summary>
        /// Возвращает FlagColor по названию.
        /// </summary>
        /// <param name="color">Название цвета</param>
        public static FlagColor ConvertFlagColorFromString(string color)
        {
            switch (color)
            {
                case "Red": return FlagColor.Red;
                case "Green": return FlagColor.Green;
                case "Blue": return FlagColor.Blue;
                case "Yellow": return FlagColor.Yellow;
                case "Pink": return FlagColor.Pink;
                case "Black": return FlagColor.Black;

                default: return FlagColor._No_Color_;
            }
        }
    }
}
