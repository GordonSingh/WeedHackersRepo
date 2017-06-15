using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeedHackers_Data.ServiceProcess;
using WeedHackers_Data.UserTypes;

namespace WeedHackers_Data.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key, ForeignKey("User")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a Department")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        [Required(ErrorMessage = "Please select an Employee Type")]
        public int EmployeeTypeId { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [ForeignKey("EmployeeTypeId")]
        public EmployeeType EmployeeType { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Department> ManagingDepartments { get; set; } = new List<Department>();

        public virtual ICollection<ServiceRequest> AssignedAsLeadServiceRequests { get; set; } = new List<ServiceRequest>();

        public virtual ICollection<ServiceRequest> AssignedServiceRequests { get; set; } = new List<ServiceRequest>();

        public virtual ICollection<ServiceRequest> AssignedAsServiceAdvisorServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}
