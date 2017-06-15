using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeedHackers_Data.Entities;

namespace WeedHackers_Data.UserTypes
{
    [Table("EmployeeTypes")]
    public class EmployeeType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
