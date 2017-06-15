using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using DS32017_WeedHackers.Models.ServiceModels;
using WeedHackers_Data.Entities;
using WeedHackers_Data.ServiceProcess;

namespace DS32017_WeedHackers.Models.CustomerModels
{
    public class CustomerInformationModel : SecondaryBaseModel
    {
        public User CurrentCustUser { get; set; }
        public Customer CurrentCustomer { get; set; }

        public CustomerType CurreCustomerType { get; set; }
        public ServiceRequestModel ServiceRequestModel { get; set; }

        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
         ErrorMessage = "Invalid Email Address Entered")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "Phone Number")]
        public string phonenumber { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        

    }
}