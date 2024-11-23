using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Exceptions
{
    internal class PayrollGenerationException : ApplicationException
    {
        public PayrollGenerationException()
        {
            
        }
        public PayrollGenerationException(string message) : base(message)
        {
            
        }
    }
}
