using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeedHackers_Data.ServiceProcess;

namespace WeedHackers_Data.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key, ForeignKey("User")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Address is required for Service Requests. Please Enter a valid Address")]
        public string Address { get; set; }

        //Customer Type is Commercial(Business) or SingleUser(Average customer)
        [Required]
        public int CustomerTypeId { get; set; }

        [Required]
        public bool EmailVerified { get; set; } = false;

        [Required]
        public bool Deleted { get; set; } = false;

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [ForeignKey("CustomerTypeId")]
        public CustomerType CustomerType { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
