using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_coreWebApplication.Models;

namespace test_coreWebApplication.Pages
{
    public class EditEmployeeModel : PageModel
    {
        private readonly DataAccess.DataAccess objDB;
        public EditEmployeeModel(DataAccess.DataAccess _objDB)
        {
            objDB = _objDB;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public void OnGet(string EmployeeID)
        {
            Employee = objDB.SelectDatabyID(EmployeeID);
        }
        public IActionResult OnPostUpdate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string result = objDB.UpdateData(Employee);
            TempData["toastSuccess"] = "Update";
            return RedirectToPage("Employee");
        }
    }
}
