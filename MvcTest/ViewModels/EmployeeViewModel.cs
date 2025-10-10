using Mvc.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.PAL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Length is 50 chars.")]
        [MinLength(5, ErrorMessage = "Minimum Length is 5 char.")]
        public string Name { get; set; }
        [Range(22, 35, ErrorMessage = "Age must be in range 22 To 35.")]
        public int Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-street-city-country.")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public int? DepartmentId { get; set; }

        [InverseProperty("Employees")]
        public Department Department { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }
    
    }
}
