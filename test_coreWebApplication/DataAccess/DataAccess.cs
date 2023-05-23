using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using test_coreWebApplication.Models;

namespace test_coreWebApplication.DataAccess
{
    public class DataAccess
    {
        private readonly IConfiguration configuration;

        public DataAccess(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string InsertData(Employee objemp)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                var connectionString = configuration.GetConnectionString("mycon");
                con = new SqlConnection(connectionString);
                var param = new DynamicParameters();
                param.Add("@EmployeeName", objemp.EmployeeName);
                param.Add("@Address", objemp.Address);
                param.Add("@Mobileno", objemp.Mobileno);
                param.Add("@Password", objemp.Password);
                param.Add("@EmailID", objemp.EmailID);
                param.Add("@Query", 1);
                con.Open();
                result = con.Execute("Usp_InsertUpdateDelete_Employee", param, null, 0, commandType: CommandType.StoredProcedure).ToString();
                return result;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }
        public string UpdateData(Employee objemp)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                var connectionString = configuration.GetConnectionString("mycon");
                con = new SqlConnection(connectionString);
                var param = new DynamicParameters();
                param.Add("@EmployeeName", objemp.EmployeeName);
                param.Add("@EmployeeId", objemp.EmployeeId);
                param.Add("@Address", objemp.Address);
                param.Add("@Mobileno", objemp.Mobileno);
                param.Add("@Password", objemp.Password);
                param.Add("@EmailID", objemp.EmailID);
                param.Add("@Query", 2);
                result = con.Execute("Usp_InsertUpdateDelete_Employee", param, null, 0, commandType: CommandType.StoredProcedure).ToString();
                con.Open();
                return result;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }
        public int DeleteData(String ID)
        {
            SqlConnection con = null;
            int result;
            try
            {
                var connectionString = configuration.GetConnectionString("mycon");
                con = new SqlConnection(connectionString);
                con.Open();
                var param = new DynamicParameters();
                param.Add("@EmployeeId", ID);
                param.Add("@EmployeeName", null);
                param.Add("@Address", null);
                param.Add("@Mobileno", null);
                param.Add("@Mobileno", null);
                param.Add("@Password", null);
                param.Add("@Query", 3);
                result = con.Execute("Usp_InsertUpdateDelete_Employee", param, null, 0, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return result = 0;
            }
            finally
            {
                con.Close();
            }
        }
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
                employeelist = con.Query<Employee>("Usp_InsertUpdateDelete_Employee",param,null ,true,0,commandType: CommandType.StoredProcedure).ToList();
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

        public Employee SelectDatabyID(string EmployeeId)
        {
            SqlConnection con = null;
            DataSet ds = null;
            Employee empobj = null;
            try
            {
                var connectionString = configuration.GetConnectionString("mycon");
                con = new SqlConnection(connectionString);
                var param = new DynamicParameters();
                param.Add("@EmployeeId", EmployeeId);
                param.Add("@EmployeeName", null);
                param.Add("@Address", null);
                param.Add("@Mobileno", null);
                param.Add("@Mobileno", null);
                param.Add("@Password", null);
                param.Add("@Query", 5);
                con.Open();
                empobj = con.Query<Employee>("Usp_InsertUpdateDelete_Employee", param, null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return empobj;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return empobj;
            }
            finally
            {
                con.Close();
            }
        }
         
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

