using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeedHackers_Data.Entities;

namespace WeedHackers_Data.ServiceProcess
{
    [Table("ServiceStatuses")]
    public class ServiceStatus : BaseEntity
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(500)]
        public string Name { get; set; }

        public virtual ICollection<ServiceRequestStatusUpdate> ServiceRequestStatusUpdates { get; set; } = new List<ServiceRequestStatusUpdate>();
    }
}
