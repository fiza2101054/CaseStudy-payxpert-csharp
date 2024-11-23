using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Model
{
    public class Tax
    {

        private int _taxID;
        private int _employeeID;
        private int _taxYear;
        private decimal _taxableIncome;
        private decimal _taxAmount;

        // Properties
        public int TaxID
        {
            get { return _taxID; }
            set { _taxID = value; }
        }

        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        public int TaxYear
        {
            get { return _taxYear; }
            set { _taxYear = value; }
        }

        public decimal TaxableIncome
        {
            get { return _taxableIncome; }
            set { _taxableIncome = value; }
        }

        public decimal TaxAmount
        {
            get { return _taxAmount; }
            set { _taxAmount = value; }
        }

        public override string ToString()
        {
            return $"Tax ID: {TaxID}\t" +
                   $"Employee ID: {EmployeeID}\t" +
                   $"Tax Year: {TaxYear}\t" +
                   $"Taxable Income: {_taxableIncome:C2}\t" +
                   $"Tax Amount: {_taxAmount:C2}";
        }
    }
}
