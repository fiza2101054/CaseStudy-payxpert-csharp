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
    public class PayrollRepository : IPayrollRepository
    {
        private readonly string connectionString;
        private SqlCommand _cmd;

        public PayrollRepository()
        {
            connectionString = DBConnUtil.GetConnectionString(); 
            _cmd = new SqlCommand();
        }

        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)
        {
            Payroll payroll = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = @"
                        SELECT PayrollId, EmployeeId, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary
                        FROM Payroll 
                        WHERE EmployeeId = @EmployeeId 
                        AND PayPeriodStartDate >= @StartDate
                        AND PayPeriodEndDate <= @EndDate; SELECT SCOPE_IDENTITY();";

                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    _cmd.Parameters.AddWithValue("@StartDate", startDate);
                    _cmd.Parameters.AddWithValue("@EndDate", endDate);
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        payroll = new Payroll
                        {
                            PayrollID = (int)reader["PayrollId"],
                            EmployeeID = (int)reader["EmployeeId"],
                            PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                            PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                            BasicSalary = (decimal)reader["BasicSalary"],
                            OvertimePay = (decimal)reader["OvertimePay"],
                            Deductions = (decimal)reader["Deductions"],
                            //NetSalary = (decimal)reader["NetSalary"]

                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return payroll;
        }

        public Payroll GetPayrollById(int payrollId)
        {
            Payroll payroll = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Payroll WHERE PayrollID = @PayrollID", conn);
                cmd.Parameters.AddWithValue("@PayrollID", payrollId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    payroll = new Payroll
                    {
                        PayrollID = (int)reader["PayrollID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                        PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                        BasicSalary = (decimal)reader["BasicSalary"],
                        OvertimePay = (decimal)reader["OvertimePay"],
                        Deductions = (decimal)reader["Deductions"]
                       
                    };
                }
            }

            return payroll;
        }


        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = "SELECT * FROM Payroll WHERE EmployeeID = @EmployeeID";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Payroll payroll = new Payroll
                        {
                            PayrollID = (int)reader["PayrollID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                            PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                            BasicSalary = (decimal)reader["BasicSalary"],
                            OvertimePay = (decimal)reader["OvertimePay"],
                            Deductions = (decimal)reader["Deductions"]
                           
                        };
                        payrolls.Add(payroll);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return payrolls;
        }


        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = "SELECT * FROM Payroll WHERE PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@StartDate", startDate);
                    _cmd.Parameters.AddWithValue("@EndDate", endDate);
                    _cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader reader = _cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Payroll payroll = new Payroll
                            {
                                PayrollID = (int)reader["PayrollID"],
                                EmployeeID = (int)reader["EmployeeID"],
                                PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                                PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                                BasicSalary = (decimal)reader["BasicSalary"],
                                OvertimePay = (decimal)reader["OvertimePay"],
                                Deductions = (decimal)reader["Deductions"]
                            };
                            payrolls.Add(payroll);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return payrolls;
        }


    }
}
