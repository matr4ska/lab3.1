using ClassLibrary.ModelEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IHelper
    {
        public event EventHandler<OnHelpTextRequestedEventArgs> OnHelpTextRequested;

        /// <summary>
        /// Выводит текст о том, как пользоваться приложением
        /// </summary>
        /// <returns>Текст с помощью</returns>
        public abstract void GetHelpText();
    }
}
