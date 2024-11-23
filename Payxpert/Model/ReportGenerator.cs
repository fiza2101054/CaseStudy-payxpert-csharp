using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Model
{
    internal class ReportGenerator
    {
       // #region Employee Details
        private Employee _employeeDetails;
        private List<Payroll> _payrollHistory;
        private List<Tax> _taxHistory;
        private List<FinancialRecord> _financialRecords;

        public Employee EmployeeDetails
        {
            get { return _employeeDetails; }
            set { _employeeDetails = value; }
        }

        public List<Payroll> PayrollHistory
        {
            get { return _payrollHistory; }
            set { _payrollHistory = value; }
        }

        public List<Tax> TaxHistory
        {
            get { return _taxHistory; }
            set { _taxHistory = value; }
        }

        public List<FinancialRecord> FinancialRecords
        {
            get { return _financialRecords; }
            set { _financialRecords = value; }
        }
       // #endregion

        // Constructor to initialize the lists
        public ReportGenerator()
        {
            PayrollHistory = new List<Payroll>();
            TaxHistory = new List<Tax>();
            FinancialRecords = new List<FinancialRecord>();
        }
    }
}
