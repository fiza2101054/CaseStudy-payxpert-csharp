using Payxpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Repository
{
    public interface IPayrollRepository
    {

        Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate);
        Payroll GetPayrollById(int payrollId);

        List<Payroll> GetPayrollsForEmployee(int employeeId);

        List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);


    }
}
