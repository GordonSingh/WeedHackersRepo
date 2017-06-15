using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data.Entities;
using WeedHackers_Data.UserTypes;

namespace DS32017_WeedHackers.Models.EmployeeModels.Admin
{
    public class AdminModel : BaseModel
    {
        public List<NavigationModel> SideNavModels { get; set; } = new List<NavigationModel>();
        public WeedHackers_Data.Entities.Employee AdminEmployee { get; set; }
        public User AdminUser { get; set; }

        #region AdminFunctionModels
        public List<User> AllUsers { get; set; } = new List<User>();
        public List<Customer> AllCustomers { get; set; } = new List<Customer>();
        public List<WeedHackers_Data.Entities.Employee> AllEmployees { get; set; } = new List<WeedHackers_Data.Entities.Employee>();
        public List<Department> AllDepartments { get; set; } = new List<Department>();

        public List<EmployeeType> AllEmployeeTypes { get; set; }=new List<EmployeeType>();
        #endregion

        public WeedHackers_Data.Entities.Employee NewEmployee { get; set; }
        public WeedHackers_Data.Entities.Customer NewCustomer { get; set; }
        public User NewUser { get; set; }

        #region UpdateFields
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        ErrorMessage = "Invalid Email Address Entered")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "Phone Number")]
        public string phonenumber { get; set; }

        #endregion

    }
}