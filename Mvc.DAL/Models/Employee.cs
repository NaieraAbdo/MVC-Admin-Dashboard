using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        //[Required]
        //[MaxLength(50)
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Length is 50 chars.")]
        [MinLength(5, ErrorMessage = "Minimum Length is 5 char.")]

        public string Name { get; set; }
        [Range(22, 35, ErrorMessage = "Age must be in range 22 To 35.")]

        public int Age { get; set; } 
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int? DepartmentId { get; set; }

        [InverseProperty("Employees")]
        public Department Department { get; set; }
        //public string? ImageName { get; set; }

    }
}
