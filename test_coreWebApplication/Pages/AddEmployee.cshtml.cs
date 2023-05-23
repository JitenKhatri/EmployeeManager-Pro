using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_coreWebApplication.Models;

namespace test_coreWebApplication.Pages
{
    public class AddEmployeeModel : PageModel
    {
        private readonly DataAccess.DataAccess objDB;
        public AddEmployeeModel(DataAccess.DataAccess _objDB)
        {
            objDB = _objDB;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string result = objDB.InsertData(Employee);
            TempData["toastSuccess"] = "Insert";

            return RedirectToPage("Employee");
        }
    }
}
