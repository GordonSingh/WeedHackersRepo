using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeedHackers_Data.Entities
{
    [Table("CustomerTypes")]
    public class CustomerType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
