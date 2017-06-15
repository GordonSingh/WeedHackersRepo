using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeedHackers_Data.Entities
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be blank.")]
        [Display(Name = "Name")]
        [Index("IX_FullName", 1, IsUnique = true)]
        [StringLength(500)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname cannot be blank.")]
        [Display(Name = "Surname")]
        [Index("IX_FullName", 2, IsUnique = true)]
        [StringLength(500)]

        public string Surname { get; set; }

        [Required(ErrorMessage = "Email cannot be blank.")]
        [Display(Name = "Email")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        ErrorMessage = "Invalid Email Address Entered")]
        [Index(IsUnique = true)]
        [StringLength(500)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be blank!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 alphanumerical characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Contact Number cannot be blank.")]
        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public bool SuperAdmin { get; set; } = false;

        [Required]
        public bool Deleted { get; set; } = false;

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public virtual Employee Employee { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
