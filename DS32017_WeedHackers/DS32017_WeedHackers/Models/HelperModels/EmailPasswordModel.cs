using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Models.HelperModels
{
    public class EmailPasswordModel:BaseModel
    {
        [Required(ErrorMessage = "Please Enter Your Registered Email Address")]
        
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
         ErrorMessage = "Invalid Email Address Entered")]
        public string Email { get; set; }
       
    }
}