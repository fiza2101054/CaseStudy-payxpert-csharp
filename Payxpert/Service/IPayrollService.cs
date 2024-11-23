using Payxpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{
    public interface IPayrollService
    {
        void  GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate);
        void GetPayrollById(int payrollId);
        void GetPayrollsForEmployee(int employeeId);
        void GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
        void CalculateGrossSalaryForEmployee(int employeeId);


    }
}
