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
    public class TaxRepository : ITaxRepository
    {

        private readonly string connectionString;
        private SqlCommand _cmd;

        public TaxRepository()
        {
            connectionString = DBConnUtil.GetConnectionString();
            _cmd = new SqlCommand();
        }

        public Tax GetTaxById(int taxId)
        {
            Tax tax = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = "SELECT * FROM Tax WHERE TaxId = @TaxId";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@TaxId", taxId);
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tax = ExtractTax(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return tax;
        }


        public List<Tax> GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxes = new List<Tax>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = "SELECT * FROM Tax WHERE EmployeeID = @EmployeeID";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        taxes.Add(ExtractTax(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return taxes;
        }



        public List<Tax> GetTaxesForYear(int taxYear)
        {
            List<Tax> taxes = new List<Tax>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = "SELECT * FROM Tax WHERE TaxYear = @TaxYear";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        taxes.Add(ExtractTax(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return taxes;
        }

        public decimal CalculateTax(int employeeId, int taxYear)
        {
            decimal totalTax = 0;
            List<Tax> taxes = GetTaxesForEmployee(employeeId); 

            foreach (var tax in taxes)
            {
                if (tax.TaxYear == taxYear)
                {
                    decimal taxableIncome = tax.TaxableIncome;
                    totalTax += CalculateTaxForIncome(taxableIncome);   
                }
            }
            return totalTax;
        }

        public decimal CalculateTaxForIncome(decimal taxableIncome)
        {
            decimal taxAmount = 0;

            if (taxableIncome > 0 && taxableIncome <= 250000)
            {
                taxAmount = 0; 
            }
            else if (taxableIncome > 250000 && taxableIncome <= 500000)
            {
                taxAmount = (taxableIncome - 250000) * 0.05m; 
            }
            else if (taxableIncome > 500000 && taxableIncome <= 1000000)
            {
                taxAmount = (250000 * 0.05m) + 
                            (taxableIncome - 500000) * 0.1m; 
            }
            else if (taxableIncome > 1000000)
            {
                taxAmount = (250000 * 0.05m) + 
                            (500000 * 0.1m) + 
                            (taxableIncome - 1000000) * 0.2m; 
            }

            return taxAmount;
        }





        private Tax ExtractTax(SqlDataReader reader)
        {
            return new Tax
            {
                TaxID = (int)reader["TaxId"],
                EmployeeID = (int)reader["EmployeeID"],
                TaxYear = (int)reader["TaxYear"],
                TaxableIncome = (decimal)reader["TaxableIncome"],
                TaxAmount = (decimal)reader["TaxAmount"]
            };
        }
    }
}
