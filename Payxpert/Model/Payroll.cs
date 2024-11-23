using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Model
{
    public class Payroll
    {
        private int _payrollID;
        private int _employeeID;
        private DateTime _payPeriodStartDate;
        private DateTime _payPeriodEndDate;
        private decimal _basicSalary;
        private decimal _overtimePay;
        private decimal _deductions;
        private decimal _netSalary;
        internal decimal netSalary;

        // Properties
        public int PayrollID
        {
            get { return _payrollID; }
            set { _payrollID = value; }
        }

        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        public DateTime PayPeriodStartDate
        {
            get { return _payPeriodStartDate; }
            set { _payPeriodStartDate = value; }
        }

        public DateTime PayPeriodEndDate
        {
            get { return _payPeriodEndDate; }
            set { _payPeriodEndDate = value; }
        }

        public decimal BasicSalary
        {
            get { return _basicSalary; }
            set { _basicSalary = value; }
        }

        public decimal OvertimePay
        {
            get { return _overtimePay; }
            set { _overtimePay = value; }
        }

        public decimal Deductions
        {
            get { return _deductions; }
            set { _deductions = value; }
        }

        public decimal NetSalary
        {
            get { return CalculateNetSalary(); }
        }

        private decimal CalculateNetSalary()
        {
            _netSalary = BasicSalary + OvertimePay - Deductions;
            return _netSalary;
        }
        public override string ToString()
        {
            return $"Payroll ID: {PayrollID}\t" +
                   $"Employee ID: {EmployeeID}\t" +
                   $"Pay Period: {PayPeriodStartDate:yyyy-MM-dd} to {PayPeriodEndDate:yyyy-MM-dd}\t" +
                   $"Net Salary: {NetSalary}";
        }
    }
}
