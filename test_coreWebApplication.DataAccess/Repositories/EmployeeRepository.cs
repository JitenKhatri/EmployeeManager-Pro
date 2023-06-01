using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using test_coreWebApplication.DataAccess.Repositories.Interfaces;
using test_coreWebApplication.Entities.Entities;

namespace test_coreWebApplication.DataAccess.Repositories
{
    public class EmployeeRepository :IEmployeeRepository
    {
        private readonly IConfiguration configuration;

        public EmployeeRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        
        /// <summary>
        /// A simple method to simply get all the employeedata from the employee table
        /// </summary>
        /// <returns>
        /// List of employee objects</returns>
        public List<Employee> Selectalldata()
        {
            SqlConnection con = null;
            List<Employee> employeelist = new List<Employee>();
            try
            {
                var connectionString = configuration.GetConnectionString("mycon");
                con = new SqlConnection(connectionString);
                var param = new DynamicParameters();
                param.Add("@EmployeeId", null);
                param.Add("@EmployeeName", null);
                param.Add("@Address", null);
                param.Add("@Mobileno", null);
                param.Add("@Mobileno", null);
                param.Add("@Password", null);
                param.Add("@Query", 4);
                con.Open();
                employeelist = con.Query<Employee>("Usp_InsertUpdateDelete_Employee", param, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return employeelist;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return employeelist;
            }
            finally
            {
                con.Close();
            }
        }
        
        /// <summary>
        /// Method to handle the exceptions , it logs all the exception details into Errorlog file.
        /// </summary>
        /// <param name="ex">Exception</param>
        public void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "ErrorLogs", "ErrorLog");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
        
    }
}
