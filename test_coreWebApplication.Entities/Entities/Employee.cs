using System.ComponentModel.DataAnnotations;

namespace test_coreWebApplication.Entities.Entities
{
    public class Employee : IValidatableObject
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Enter Your Name")]
        [StringLength(10, ErrorMessage = "Name should be less than or equal to ten characters.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Enter Your Address")]
        [StringLength(80, ErrorMessage = "Address should be less than or equal to 80 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [Range(1000000000, 9999999999, ErrorMessage = "Phone number has to have only/must 10 positive numbers")]
        public long Mobileno { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^((([!#$%&'*+\-/=?^_`{|}~\w])|([!#$%&'*+\-/=?^_`{|}~\w][!#$%&'*+\-/=?^_`{|}~\.\w]{0,}[!#$%&'*+\-/=?^_`{|}~\w]))[@]\w+([-.]\w+)*\.\w+([-.]\w+)*)$", ErrorMessage = "Please enter a valid Email")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,15}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character and not longer than 15")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public long CountryId { get; set; }
        public long CityId { get; set; }

        public DateTime DateOfBirth { get; set; } = new DateTime();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (DateOfBirth > DateTime.Today.AddYears(-18))
            {
                results.Add(new ValidationResult("Employee must be at least 18 years old.", new[] { "DateOfBirth" }));
            }
            if(DateOfBirth < DateTime.Today.AddYears(-70))
            {
                results.Add(new ValidationResult("Employee must not be more than 70 years old", new[] { "DateOfBirth" }));
            }
            return results;
        }
    }
}

