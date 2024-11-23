using Payxpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Repository
{
    internal interface IReportGeneratorRepository
    {
        ReportGenerator GetReportForEmployee(int employeeId);
    }
}
