using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Models.CustomerModels
{
    public class UpdatePasswordModel:BaseModel
    {
        public int UserId { get; set; }

        public User User { get; set; }

        [Required(ErrorMessage = "Incorrect Password")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Please fill in")]
        public string NewPassword { get; set; }
        [Compare("NewPassword",ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "Please fill in ")]
        public string ConfirmPassword { get; set; }

        public UpdatePasswordModel()
        {
            User=new WeedHackersContext().Users.ToList().Find(u=>u.Id==UserId);
        }
    }
}