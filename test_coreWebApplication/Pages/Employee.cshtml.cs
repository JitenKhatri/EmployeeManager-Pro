using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using test_coreWebApplication.DataAccess.Repositories.Interfaces;
using test_coreWebApplication.Entities.Entities;

namespace test_coreWebApplication.Pages
{
    
    public class ShowAllEmployeeDetailsModel : PageModel
    {
        private readonly IUnitOfWork objDB;
        private readonly IConfiguration configuration;
        public ShowAllEmployeeDetailsModel(IUnitOfWork _objDB, IConfiguration _configuration)
        {
            objDB = _objDB;
            configuration = _configuration;
        }
        
        
        [BindProperty]
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public void OnGet(int start = 0, int length = 10)
        {
            Employees = objDB.EmployeeRepository.Selectalldata();
        }


        public IActionResult OnPostGetEmployees()
        {
            // Perform server-side pagination using the stored procedure and Dapper
            List<Employee> employees = new List<Employee>();
            int totalRecords = 0;
            int filteredRecords = 0;
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var draw = Request.Form["draw"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            string emailSearch = Request.Form["columns[1][search][value]"];
            string employeeIdSearch = Request.Form["columns[0][search][value]"];
            string employeeNameSearch = Request.Form["columns[2][search][value]"];
            string addressSearch = Request.Form["columns[3][search][value]"];
            string mobileNoSearch = Request.Form["columns[4][search][value]"];
            string dateOfBirthSearch = Request.Form["columns[5][search][value]"];
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("mycon")))
            {
                con.Open();
                // Retrieve the paginated data
                var p = new DynamicParameters();
                p.Add("@Start", start); 
                p.Add("@Length", length);
                p.Add("@SearchString", searchValue);
                p.Add("@TotalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@FilteredRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@SortColumn", sortColumn);
                p.Add("@SortColumnDirection", sortColumnDirection);
                p.Add("@EmailSearchString", emailSearch);
                p.Add("@EmployeeNameSearchString", employeeNameSearch);
                p.Add("@AddressSearchString", addressSearch);
                p.Add("@MobileNoSearchString", mobileNoSearch);
                p.Add("@EmployeeIdSearchString", employeeIdSearch);
                employees = con.Query<Employee>("Usp_GetEmployees_Paginated", p, commandType: CommandType.StoredProcedure).ToList();
                totalRecords = p.Get<int>("@TotalRecords");
                filteredRecords = p.Get<int>("@FilteredRecords");
            }
            // Create a response object with the paginated data
            var response = new
            {
                draw = draw, // Hardcoded value or retrieve it from the request
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = employees
            };

            return new JsonResult(response);
        }


        public IActionResult OnPostDelete(string EmployeeId)
        {
            int result = objDB.EmployeeRepository.DeleteData(EmployeeId);
            TempData["toastError"] = result;
            return RedirectToPage();
        }
    }
}
