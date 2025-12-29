using ClassLibrary.Interfaces;
using ClassLibrary.ModelEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Logic
{
    public class Helper : IHelper
    {
        public event EventHandler<OnHelpTextRequestedEventArgs> OnHelpTextRequested;


        public void GetHelpText()
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
            }

            OnHelpTextRequested(this, new OnHelpTextRequestedEventArgs(result));
        }
    }
}
