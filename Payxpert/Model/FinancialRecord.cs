using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Model
{
    internal class FinancialRecord
    {
        private int _recordID;
        private int _employeeID;
        private DateTime _recordDate;
        private string? _description;
        private decimal _amount;
        private string? _recordType;

        // Properties
        public int RecordID
        {
            get { return _recordID; }
            set { _recordID = value; }
        }

        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        public DateTime RecordDate
        {
            get { return _recordDate; }
            set { _recordDate = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string RecordType
        {
            get { return _recordType; }
            set { _recordType = value; }
        }

        // Override ToString for Displaying Details
        public override string ToString()
        {
            return $"Record ID: {RecordID}\t" +
                   $"Employee ID: {EmployeeID}\t" +
                   $"Date: {RecordDate:yyyy-MM-dd}\t" +
                   $"Description: {Description}\t" +
                   $"Amount: {_amount}\t" +
                   $"Type: {RecordType}";
        }
    }
}
