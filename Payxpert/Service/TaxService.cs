using Payxpert.Exceptions;
using Payxpert.Model;
using Payxpert.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{
    public class TaxService : ITaxService
    {
        private readonly ITaxRepository _taxRepository;

        //public TaxService(ITaxRepository taxRepository)
        //{
        //    _taxRepository = taxRepository;
        //}
        public TaxService()
        {
            _taxRepository = new TaxRepository();
        }

        public void GetTaxById(int taxId)
        {
            Tax tax = _taxRepository.GetTaxById(taxId);
            if (tax == null)
            {
                Console.WriteLine($"Tax record not found with ID: {taxId}");
            }
            else
            {
                Console.WriteLine($"Tax Found: {tax}");
            }
        }
        public void GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxes = _taxRepository.GetTaxesForEmployee(employeeId);
            if (taxes == null || taxes.Count == 0)
            {
                Console.WriteLine($"No tax records found for Employee ID: {employeeId}");
            }
            else
            {
                Console.WriteLine($"Taxes for Employee ID: {employeeId}");
                foreach (var tax in taxes)
                {
                    Console.WriteLine($"Tax Year: {tax.TaxYear}, Taxable Income: {tax.TaxableIncome}, Tax Amount: {tax.TaxAmount}");
                }
            }
        }


        public void GetTaxesForYear(int taxYear)
        {
            List<Tax> taxes = _taxRepository.GetTaxesForYear(taxYear);
            if (taxes == null || taxes.Count == 0)
            {
                Console.WriteLine($"No tax records found for Tax Year: {taxYear}");
            }
            else
            {
                Console.WriteLine($"Taxes for Tax Year: {taxYear}");
                foreach (var tax in taxes)
                {
                    Console.WriteLine($"Employee ID: {tax.EmployeeID}, Taxable Income: {tax.TaxableIncome}, Tax Amount: {tax.TaxAmount}");
                }
            }

        }

        public void CalculateTax(int employeeId, int taxYear)
        {
            try
            {
                if (employeeId <= 0)
                {
                    throw new TaxCalculationException("Invalid Employee ID: Employee ID must be greater than zero.");
                }

                if (taxYear > DateTime.Now.Year)
                {
                    throw new TaxCalculationException($"Invalid Tax Year: Tax year {taxYear} cannot be greater than the current year {DateTime.Now.Year}.");
                }


                decimal totalTax = _taxRepository.CalculateTax(employeeId, taxYear);
                Console.WriteLine($"Total Tax for Employee ID: {employeeId} for Year {taxYear} is: {totalTax}");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                //throw new TaxCalculationException($"An error occurred while calculating tax: {ex.Message}");
            }
        }


    }
}
