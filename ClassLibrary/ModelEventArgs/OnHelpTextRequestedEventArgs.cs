using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ModelEventArgs
{
    public class OnHelpTextRequestedEventArgs : EventArgs
    {
        public string HelpText { get; set; }

        public OnHelpTextRequestedEventArgs(string helpText)
        {
            HelpText = helpText;
        }
    }
}
