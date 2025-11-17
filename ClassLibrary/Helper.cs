using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Helper
    {
        /// <summary>
        /// Выводит текст о том, как пользоваться приложением.
        /// </summary>
        /// <returns>Текст с помощью.</returns>
        public static string GetHelpText()
        {
            string result = @"";
            using (var reader = new StreamReader(@"help.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result += line;
                    result += Environment.NewLine;
                }
                return result;
            }
        }
    }
}
