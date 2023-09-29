using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ExceptionMessage : Exception
    {
        public ExceptionMessage(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
