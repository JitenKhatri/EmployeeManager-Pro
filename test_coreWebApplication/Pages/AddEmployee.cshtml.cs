using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using test_coreWebApplication.DataAccess.Repositories.Interfaces;
using test_coreWebApplication.Entities.Entities;
using test_coreWebApplication.Utilities;

namespace test_coreWebApplication.Pages
{
    public class AddEmployeeModel : PageModel
    {
        string Baseurl = "https://localhost:7227/gateway/Employee/";
        private readonly IUnitOfWork objDB;
        private readonly HttpClientHelper _httpClientHelper;
        public AddEmployeeModel(IUnitOfWork _objDB)
        {
            objDB = _objDB;
            _httpClientHelper = new HttpClientHelper();
        }
        [BindProperty(SupportsGet = true)]
        public Employee Employee { get; set; }
        public SelectList Countries { get; set; }
        public SelectList Cities { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public long CountryId { get; set; }

        /// <summary>
        /// get method here provides form datas for two operations "ADD" and "EDIT"
        /// </summary>
        /// <param name="EmployeeID">
        /// EmployeeID is 0 that means loading the ADD FORM
        /// and IF IT IS other than 0 loading the EDIT FORM With respective data
        /// </param>
        public async Task<IActionResult> OnGet(string EmployeeID)
        {
            var countryapiresponse = await _httpClientHelper.GetAsync("https://localhost:7227/gateway/Country/GetCountries");
            Countries = new SelectList(JsonConvert.DeserializeObject<IEnumerable<Country>>(countryapiresponse), nameof(Country.CountryId), nameof(Country.CountryName));
            var cityapiresponse = await _httpClientHelper.GetAsync("https://localhost:7227/gateway/Country/GetCityforcountry/0");
            Cities = new SelectList(JsonConvert.DeserializeObject<IEnumerable<City>>(cityapiresponse), nameof(City.CityId), nameof(City.CityName));
            var apiResponse = await _httpClientHelper.GetAsync(Baseurl +"GetEmployeeById/" + EmployeeID);
            try
            {
                Employee = JsonConvert.DeserializeObject<Employee>(apiResponse);
            }
            catch(Exception ex)
            {
                objDB.EmployeeRepository.LogError(ex);
            }
            if(EmployeeID == "0")
            {
                Employee = new Employee();
                Employee.EmployeeId = 0;
            }
            return Page();
        }

        /// <summary>
        /// Again a general method to handle the "ADD" and EDIT" Requests using external api
        /// </summary>
        /// <returns>it  redirects to list of objects page on success</returns>
        public async Task<IActionResult> OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                var countryapiresponse = await _httpClientHelper.GetAsync("https://localhost:7227/gateway/Country/GetCountries");
                Countries = new SelectList(JsonConvert.DeserializeObject<IEnumerable<Country>>(countryapiresponse), nameof(Country.CountryId), nameof(Country.CountryName));
                var cityapiresponse = await _httpClientHelper.GetAsync("https://localhost:7227/gateway/Country/GetCityforcountry/0");
                Cities = new SelectList(JsonConvert.DeserializeObject<IEnumerable<City>>(cityapiresponse), nameof(City.CityId), nameof(City.CityName));
                return Page();
            }
            if(Employee.EmployeeId == null || Employee.EmployeeId == 0)
            {
                var response = await _httpClientHelper.PostAsync(Baseurl +"AddEmployee", Employee);
                TempData["toastSuccess"] = "Insert";
                return RedirectToPage("Employee");
            }
            else
            {
                var response = await _httpClientHelper.PutAsync(Baseurl +"UpdateEmployee", Employee);
                TempData["toastSuccess"] = "Update";
                return RedirectToPage("Employee");
            }
           
        }
        /// <summary>
        /// Method to cascade city list based on selected country
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns>000000
        /// json objects including city class properties according to the country 
        /// </returns>
        public async Task<JsonResult> OnGetCities(long countryId)
        {
            var cityapiresponse = await _httpClientHelper.GetAsync("https://localhost:7227/gateway/Country/GetCityforcountry/" + countryId);
            var cities = JsonConvert.DeserializeObject<IEnumerable<City>>(cityapiresponse);
            return  new JsonResult(cities);
        }
    }
}
