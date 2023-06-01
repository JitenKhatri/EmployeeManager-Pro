using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using test_coreWebApplication.DataAccess.Repositories.Interfaces;
using test_coreWebApplication.Entities.Entities;
using test_coreWebApplication.Entities.InputModels;
using test_coreWebApplication.Utilities;

namespace test_coreWebApplication.Pages
{
    
    public class ShowAllEmployeeDetailsModel : PageModel
    {
        string Baseurl = "https://localhost:7227/gateway/Employee/";
        private readonly IUnitOfWork objDB;
        private readonly IConfiguration configuration;
        private readonly HttpClientHelper _httpClientHelper;
        public ShowAllEmployeeDetailsModel(IUnitOfWork _objDB, IConfiguration _configuration)
        {
            objDB = _objDB;
            configuration = _configuration;
            _httpClientHelper = new HttpClientHelper();
        }
        
        [BindProperty]
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public void OnGet()
        {
            Employees = objDB.EmployeeRepository.Selectalldata();
        }


        public async Task<IActionResult> OnPostGetEmployees()
        {
            // Perform server-side pagination using the stored procedure and Dapper
            List<Employee> employees = new List<Employee>();
            GetEmpInput filterinput = new GetEmpInput();
            int totalRecords = 0;
            int filteredRecords = 0;
            filterinput.Start = Int32.Parse(Request.Form["start"].FirstOrDefault());    
           filterinput.Length = Int32.Parse(Request.Form["length"].FirstOrDefault());
            var draw = Request.Form["draw"].FirstOrDefault();
           filterinput.SearchValue= Request.Form["search[value]"].FirstOrDefault();
            filterinput.SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            filterinput.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            filterinput.EmailSearch = Request.Form["columns[1][search][value]"];
            filterinput.EmployeeIdSearch = Request.Form["columns[0][search][value]"];
            filterinput.EmployeeNameSearch = Request.Form["columns[2][search][value]"];
            filterinput.AddressSearch = Request.Form["columns[3][search][value]"];
             filterinput.MobileNoSearch = Request.Form["columns[4][search][value]"];
            var response = await _httpClientHelper.PostAsync(Baseurl +"GetEmployees", filterinput);
            if (!string.IsNullOrEmpty(response))
            {
                JObject result = JObject.Parse(response);
                JArray employeeArray = (JArray)result.SelectToken("value.data");
                employees = employeeArray.ToObject<List<Employee>>();
                totalRecords = (int)result.SelectToken("value.recordsTotal");
                filteredRecords = (int)result.SelectToken("value.recordsFiltered");
            }

            var finalresponse = new
            {
                draw = draw, // Hardcoded value or retrieve it from the request
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = employees
            };
            return new JsonResult(finalresponse);
        }


        public async Task<IActionResult> OnPostDelete(string EmployeeId)
        {
            var response = await _httpClientHelper.DeleteAsync(Baseurl +"DeleteEmployee/" + EmployeeId);
            if (!string.IsNullOrEmpty(response))
            {
                TempData["toastError"] = "Delete";
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage();
            }
            
        }
    }
}
