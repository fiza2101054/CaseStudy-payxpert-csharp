using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{
    public interface ITaxService
    {
        void GetTaxById(int taxId);

        void GetTaxesForEmployee(int employeeId);
        void GetTaxesForYear(int taxYear);
        void CalculateTax(int employeeId, int taxYear);
    }
}
