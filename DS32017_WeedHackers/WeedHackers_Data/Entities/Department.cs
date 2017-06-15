using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeedHackers_Data.Entities
{
    [Table("Departments")]
    public class Department : BaseEntity
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(500)]
        public string DepartmentName { get; set; }

        public int? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Employee ManagingEmployee { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
