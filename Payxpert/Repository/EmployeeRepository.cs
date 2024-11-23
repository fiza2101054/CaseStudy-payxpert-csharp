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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString;
        private SqlCommand _cmd;

        public EmployeeRepository()
        {
            connectionString = DBConnUtil.GetConnectionString(); 
            _cmd = new SqlCommand();
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = "SELECT * FROM Employee";
                    _cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employees.Add(ExtractEmployee(reader));
                    }
                }
                catch (Exception ex)  
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return employees;
        }

       
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = "SELECT * FROM Employee WHERE EmployeeId = @EmployeeId";
                    _cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    _cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        employee = ExtractEmployee(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
            }

            return employee; 
        }
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = @"INSERT INTO Employee 
                                         (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate) 
                                         VALUES 
                                         (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate, @TerminationDate; SELECT SCOPE_IDENTITY();)";
                    _cmd.Connection = conn;

                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    _cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    _cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    _cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    _cmd.Parameters.AddWithValue("@Email", employee.Email);
                    _cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    _cmd.Parameters.AddWithValue("@Address", employee.Address);
                    _cmd.Parameters.AddWithValue("@Position", employee.Position);
                    _cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                    _ = _cmd.Parameters.AddWithValue("@TerminationDate", (object)employee.TerminationDate ?? DBNull.Value);


                    conn.Open();




                    // _cmd.ExecuteNonQuery(); 
                    //  Console.WriteLine("Employee added successfully!");


                    var result = _cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int newEmployeeId = Convert.ToInt32(result); 
                        Console.WriteLine($"Employee added successfully with EmployeeID: {newEmployeeId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



        public bool RemoveEmployee(int employeeId)
        {
            bool isDeleted = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    _cmd.CommandText = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    _cmd.Connection = conn;

                    conn.Open();
                    int rowsAffected = _cmd.ExecuteNonQuery();
                    isDeleted = rowsAffected > 0; 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return isDeleted;
        }



        public bool UpdateEmployee(Employee employee)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"
                UPDATE Employee
                SET 
                    FirstName = ISNULL(@FirstName, FirstName),
                    LastName = ISNULL(@LastName, LastName),
                    Position = ISNULL(@Position, Position)
                WHERE EmployeeId = @EmployeeId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeID);
                    _cmd.Parameters.AddWithValue("@FirstName", (object)employee.FirstName ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@LastName", (object)employee.LastName ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@Position", (object)employee.Position ?? DBNull.Value);

                    _cmd.Connection = conn;
                    conn.Open();

                    int rowsAffected = _cmd.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0; 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return isUpdated;
        }




        private Employee ExtractEmployee(SqlDataReader reader)
        {
            return new Employee
            {
                EmployeeID = (int)reader["EmployeeId"], 
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                DateOfBirth = (DateTime)reader["DateOfBirth"],
                Gender = (string)reader["Gender"],
                Email = (string)reader["Email"],
                PhoneNumber = (string)reader["PhoneNumber"],
                Address = (string)reader["Address"],
                Position = (string)reader["Position"],
                JoiningDate = (DateTime)reader["Joiningdate"],
                TerminationDate = reader["Terminationdate"] as DateTime? 
            };
        }



    }
}
