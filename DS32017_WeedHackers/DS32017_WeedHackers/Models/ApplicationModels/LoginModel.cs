using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Threading.Tasks;
using WeedHackers_Data;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Models.ApplicationModels
{
    public class LoginModel:BaseModel
    {
        [Required(ErrorMessage = "Please Enter a valid Email Address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        ErrorMessage = "Invalid Email Address Entered")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public User UserContext { get; set; }

     
    }
}