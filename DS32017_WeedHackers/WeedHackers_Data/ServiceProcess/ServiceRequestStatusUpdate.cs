using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeedHackers_Data.Entities;

namespace WeedHackers_Data.ServiceProcess
{
    public class ServiceRequestStatusUpdate:BaseEntity
    {
        [Required]
        public int ServiceRequestId { get; set; }

        [Required]
        public int ServiceStatusId { get; set; }

        [Required]
        public string Message { get; set; }

        [ForeignKey("ServiceRequestId")]
        public ServiceRequest ServiceRequest { get; set; }

        [ForeignKey("ServiceStatusId")]
        public ServiceStatus ServiceStatus { get; set; }
    }
}
