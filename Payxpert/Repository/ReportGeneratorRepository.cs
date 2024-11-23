using Payxpert.Model;
using Payxpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Repository
{
    internal class ReportGeneratorRepository : IReportGeneratorRepository
    {
        private readonly string connectionString;
        private SqlCommand _cmd;

        
        public ReportGeneratorRepository()
        {
            connectionString = DBConnUtil.GetConnectionString(); 
            _cmd = new SqlCommand();
        }

       
        public ReportGenerator GetReportForEmployee(int employeeId)
        {
            ReportGenerator reportGenerator = null;

           
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                   
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM Employee AS E ");
                    sb.Append("INNER JOIN Payroll AS P ON E.EmployeeID = P.EmployeeID ");
                    sb.Append("INNER JOIN Tax AS T ON E.EmployeeID = T.EmployeeID ");
                    sb.Append("INNER JOIN FinancialRecord AS F ON E.EmployeeID = F.EmployeeID ");
                    sb.Append("WHERE E.EmployeeID = @EmployeeID"); 
                    _cmd.CommandText = sb.ToString();
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EmployeeID", employeeId); 

                    
                    conn.Open();
                    using (SqlDataReader reader = _cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            reportGenerator = new ReportGenerator();

                            
                            Employee employee = new Employee
                            {
                                EmployeeID = (int)reader["EmployeeID"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],  
                                Gender = (string)reader["Gender"],
                                Email = (string)reader["Email"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                Address = (string)reader["Address"],
                                Position = (string)reader["Position"], 
                                JoiningDate = (DateTime)reader["JoiningDate"],  
                                TerminationDate = reader["TerminationDate"] as DateTime?  
                            };
                            reportGenerator.EmployeeDetails = employee;

                           
                            Payroll payroll = new Payroll
                            {
                                PayrollID = (int)reader["PayrollID"],
                                EmployeeID = (int)reader["EmployeeID"],
                                PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                                PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                                BasicSalary = (decimal)reader["BasicSalary"],
                                OvertimePay = (decimal)reader["OvertimePay"],
                                Deductions = (decimal)reader["Deductions"],
                                //NetSalary = (decimal)reader["NetSalary"]
                            };
                            reportGenerator.PayrollHistory.Add(payroll);

                           
                            Tax tax = new Tax
                            {
                                TaxID = (int)reader["TaxID"],
                                EmployeeID = (int)reader["EmployeeID"],
                                TaxYear = (int)reader["TaxYear"],
                                TaxableIncome = (decimal)reader["TaxableIncome"],
                                TaxAmount = (decimal)reader["TaxAmount"]
                            };
                            reportGenerator.TaxHistory.Add(tax);

                          
                            FinancialRecord financialRecord = new FinancialRecord
                            {
                                RecordID = (int)reader["RecordID"],
                                EmployeeID = (int)reader["EmployeeID"],
                                RecordDate = (DateTime)reader["RecordDate"],
                                Description = reader["Description"] as string,
                                Amount = (decimal)reader["Amount"],
                                RecordType = reader["RecordType"] as string
                            };
                            reportGenerator.FinancialRecords.Add(financialRecord);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return reportGenerator;
        }
    }
}
