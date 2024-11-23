using Payxpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{
    internal interface IReportGeneratorService
    {
        ReportGenerator GetReportForEmployee(int employeeId);
    }
}
