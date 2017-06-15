using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeedHackers_Data.Entities;

namespace WeedHackers_Data.ServiceProcess
{
    [Table("ServiceRequests")]
    public class ServiceRequest : BaseEntity
    {
        //Service that is associated to the service
        [Required]
        public int ServiceId { get; set; }

        //Employee that is a Service Advisor
        [Required]
        public int ServiceAdvisorId { get; set; }

        [Required]
        public int FrequencyInMonths { get; set; } = 0;

        [Required]
        public bool IsResolved { get; set; } = false;

        [Required]
        public DateTime RequestDateTime { get; set; }

        //store the Customer Requesting the service
        [Required]
        public int CustomerId { get; set; }

        public double? UnitQuantity { get; set; } = 0;
        public double? Total { get; set; } = 0;

        public int? LeadEmployeeId { get; set; }

        public int? CancellationId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("ServiceAdvisorId")]
        public Employee ServiceAdvisor { get; set; }

        [ForeignKey("CancellationId")]
        public Cancellation Cancellation { get; set; }

        [ForeignKey("LeadEmployeeId")]
        public Employee LeadEmployee { get; set; }

        public virtual ICollection<Employee> AssignedEmployees { get; set; } = new List<Employee>();

        public virtual ICollection<ServiceRequestStatusUpdate> ServiceRequestStatusUpdates { get; set; } = new List<ServiceRequestStatusUpdate>();
    }
}
