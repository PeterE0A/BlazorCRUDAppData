using BlazorCRUDApp.Entities;
using BlazorCRUDApp.Globals;
using BlazorCRUDApp.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BlazorCRUDApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private string myDbConnectionString = AccessToDb.ConnectionString;
        public bool AddEmployee(Employee employee)
        {
            SqlConnection sqlCon = null;
            String SqlconString = myDbConnectionString;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("usp_AddEmployee", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@Id", SqlDbType.UniqueIdentifier).Value = employee.Id;
                sql_cmnd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = employee.Name;
                
                int added = sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();
                if (added == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public bool DeleteEmployee(Employee employee)
        {
            SqlConnection sqlCon = null;
            String SqlconString = myDbConnectionString;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("usp_DeleteEmployee", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@Id", SqlDbType.UniqueIdentifier).Value = employee.Id;
                int deleted = sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();
                if (deleted == 1)
                {
                    return true;
                }
                return false;
            }
        }
            public Employee GetEmployee(Guid id)
            {
                Employee employee = new Employee();
                employee.Id = id;

                SqlConnection sqlCon = null;
                String SqlconString = myDbConnectionString;
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_GetEmployee", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Id", SqlDbType.UniqueIdentifier).Value = employee.Id;
                    using (SqlDataReader sdr = sql_cmnd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            employee.Id = Guid.Parse(sdr["Id"].ToString());
                            employee.Name = Convert.ToString(sdr["Name"]);
                        }
                    }
                    sqlCon.Close();
                    return employee;
                }
            }

        

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            SqlConnection sqlCon = null;
            string SqlconString = myDbConnectionString;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("usp_AllEmployees", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader sdr = sql_cmnd.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = Guid.Parse(sdr["Id"].ToString()),
                            Name = Convert.ToString(sdr["Name"]),
                        });
                    }
                }
                sqlCon.Close();
                return employees;
            }
        }


        public bool UpdateEmployee(Employee updateemployee)
        {
            SqlConnection sqlCon = null;
            String SqlconString = myDbConnectionString;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("usp_UpdateEmployee", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = updateemployee.Id;
                sql_cmnd.Parameters.AddWithValue("@Name", SqlDbType.Int).Value = updateemployee.Name;
                
                int updated = sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();
                if (updated == 1)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
