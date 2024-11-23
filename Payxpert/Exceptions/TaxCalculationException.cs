using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Exceptions
{
    internal class TaxCalculationException : ApplicationException
    {
        public TaxCalculationException()
        {
            
        }
        public TaxCalculationException(string message) : base(message) 
        {
            
        }
    }
}
