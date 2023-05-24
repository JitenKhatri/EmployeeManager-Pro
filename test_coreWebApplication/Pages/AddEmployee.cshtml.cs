using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using test_coreWebApplication.DataAccess.Repositories.Interfaces;
using test_coreWebApplication.Entities.Entities;

namespace test_coreWebApplication.Pages
{
    public class AddEmployeeModel : PageModel
    {
        private readonly IUnitOfWork objDB;
        public AddEmployeeModel(IUnitOfWork _objDB)
        {
            objDB = _objDB;
        }
        [BindProperty(SupportsGet = true)]
        public Employee Employee { get; set; }
        public SelectList Countries { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public long CountryId { get; set; }
        public void OnGet(string EmployeeID)
        {
            Countries = new SelectList(objDB.EmployeeRepository.GetallCountries(), nameof(Country.CountryId), nameof(Country.CountryName));
            Employee = objDB.EmployeeRepository.SelectDatabyID(EmployeeID);
            if(EmployeeID == "0")
            {
                Employee = new Employee();
                Employee.EmployeeId = 0;
            }
        }
        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Employee.EmployeeId == null || Employee.EmployeeId == 0)
                {
                string result = objDB.EmployeeRepository.InsertData(Employee);
                TempData["toastSuccess"] = "Insert";
                return RedirectToPage("Employee");
            }
            else
            {
                string result = objDB.EmployeeRepository.UpdateData(Employee);
                TempData["toastSuccess"] = "Update";
                return RedirectToPage("Employee");
            }
           
        }
        public JsonResult OnGetCities(long countryId)
        {
            return new JsonResult(objDB.EmployeeRepository.GetCityByCountry(countryId));
        }
    }
}
