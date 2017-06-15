using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeedHackers_Data.ServiceProcess;

namespace WeedHackers_Data.Entities
{
    [Table("Services")]
    public class Service:BaseEntity
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(500)]
        public string ServiceName { get; set; }

        [Required]
        public double PricePerUnit { get; set; } = 0;

        [Required]
        public string UnitDescription { get; set; }//grass

        [Required]
        public string UnitSuffix { get; set; }//ml, sm^2
      
        //A service belongs to a department, therefore we link that department with the service
        //Eg. Garden Maintenance service belongs to the EstateMaintenance Department
        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}
