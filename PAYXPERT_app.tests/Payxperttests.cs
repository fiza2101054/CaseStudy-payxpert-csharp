using NUnit.Framework;
using Payxpert.Exceptions;
using Payxpert.Model;
using Payxpert.Repository;
using Payxpert.Service;

namespace PAYXPERT_app.tests
{
    public class Payxperttests
    {
       
        PayrollRepository payrollRepository = new PayrollRepository();

        [Test]
        public void Test_CalculateNetSalary()
        {

            int payrollId = 1;
            Payroll payroll = payrollRepository.GetPayrollById(payrollId);
            Assert.That(payroll, Is.Not.Null, "Payroll record should not be null.");
            decimal calculatedNetSalary = payroll.BasicSalary + payroll.OvertimePay - payroll.Deductions;
            Assert.That(payroll.NetSalary, Is.EqualTo(calculatedNetSalary),
                $"Database Net Salary: {payroll.NetSalary}, Calculated Net Salary: {calculatedNetSalary}");


        }
        TaxRepository taxRepository = new TaxRepository();

        [Test]
        public void Test_VerifyTaxCalculationForHighIncomeEmployee()
        {
           
            int employeeId = 5; 
            int taxYear = 2024; 
            decimal expectedTaxAmount = 162500; 
            decimal calculatedTaxAmount = taxRepository.CalculateTax(employeeId, taxYear);
            Assert.That(calculatedTaxAmount, Is.EqualTo(expectedTaxAmount),
                $"Calculated tax: {calculatedTaxAmount}, Expected tax: {expectedTaxAmount}");
        }



        [Test]
        public void Test_GetPayrollForEmployee()
        {
            int employeeId = 1;
            Payroll expectedPayroll = new Payroll
            {
               
                EmployeeID = 1,
                PayPeriodStartDate = new DateTime(2024, 10, 01), 
                PayPeriodEndDate = new DateTime(2024, 10, 31),
                BasicSalary = 50000m, 
                OvertimePay = 5000m,
                Deductions = 2000m
            };
            var payrolls = payrollRepository.GetPayrollsForEmployee(employeeId);
            Assert.That(payrolls, Is.Not.Null.And.Count.GreaterThan(0), "Payrolls should not be null or empty.");
            var payroll = payrolls[0]; 
            Assert.That(payroll.EmployeeID, Is.EqualTo(expectedPayroll.EmployeeID));
            Assert.That(payroll.PayPeriodStartDate, Is.EqualTo(expectedPayroll.PayPeriodStartDate));
            Assert.That(payroll.PayPeriodEndDate, Is.EqualTo(expectedPayroll.PayPeriodEndDate));
            Assert.That(payroll.BasicSalary, Is.EqualTo(expectedPayroll.BasicSalary));
            Assert.That(payroll.OvertimePay, Is.EqualTo(expectedPayroll.OvertimePay));
            Assert.That(payroll.Deductions, Is.EqualTo(expectedPayroll.Deductions));
        }

        EmployeeService employeeService = new EmployeeService();

        //[Test]
        //public void Test_VerifyErrorHandlingForInvalidEmployeeData()
        //{
        //    // Arrange
        //    int invalidEmployeeId = 10; 

        //    // Act & Assert
        //    Assert.That(() => employeeService.GetEmployeeById(invalidEmployeeId),
        //                Throws.TypeOf<Payxpert.Exceptions.EmployeeNotFoundException>());
        //}

        [Test]
        public void Test_VerifyErrorHandlingForInvalidEmployeeData()
        { 
            int invalidEmployeeId = 10;
            string expectedMessage = $"Employee not found with {invalidEmployeeId}";
            var ex = Assert.Throws<Payxpert.Exceptions.EmployeeNotFoundException>(() => employeeService.GetEmployeeById(invalidEmployeeId));
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Test_CalculateGrossSalaryForEmployee()
        {
            int employeeId = 1; 
            decimal expectedGrossSalary = 55000m; 
            PayrollRepository payrollRepository = new PayrollRepository();
            var payrolls = payrollRepository.GetPayrollsForEmployee(employeeId);
            decimal calculatedGrossSalary = payrolls[0].BasicSalary + payrolls[0].OvertimePay;
            Assert.That(calculatedGrossSalary, Is.EqualTo(expectedGrossSalary),
                $"Calculated Gross Salary: {calculatedGrossSalary}, Expected Gross Salary: {expectedGrossSalary}");
        }



    }


}

