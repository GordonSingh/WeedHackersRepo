using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models;
using WeedHackers_Data.Entities;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data.ServiceProcess;
using WeedHackers_Data.UserTypes;

namespace DS32017_WeedHackers.Models.EmployeeModels.Employee
{
    public class EmployeeInformationModel:SecondaryBaseModel
    {
        public User CurrentEmpUser { get; set; }
        public WeedHackers_Data.Entities.Employee CurrentEmployee { get; set; }
        public Department DepartmentEmployee { get; set; }
        public EmployeeType currEmployeeType { get; set; }
        public List<ServiceRequest> EmpServiceRequests { get; set; }=new List<ServiceRequest>();
        
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        ErrorMessage = "Invalid Email Address Entered")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "Phone Number")]
        public string phonenumber { get; set; }
    }
}