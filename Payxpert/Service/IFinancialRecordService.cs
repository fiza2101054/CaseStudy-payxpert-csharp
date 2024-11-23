using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{
    internal interface IFinancialRecordService
    {
        void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType);
        // public void GetFinancialRecordById(int recordId);
        void GetFinancialRecordById(int recordId);

        void GetFinancialRecordsForEmployee(int employeeId);

        public void GetFinancialRecordsForDate(DateTime recordDate);
    }
}
