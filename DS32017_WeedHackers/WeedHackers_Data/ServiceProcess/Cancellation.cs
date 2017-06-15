using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeedHackers_Data.Entities;

namespace WeedHackers_Data.ServiceProcess
{
    [Table("Cancellations")]
    public class Cancellation:BaseEntity
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(500)]
        public string Reason { get; set; }

        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}
