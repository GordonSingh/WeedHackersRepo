using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Models.EmployeeModels.Admin
{
    public class AdminFunctionsModel:BaseModel
    {
        public List<User> AllUsers { get; set; }=new List<User>();
        public List<Customer> AllCustomers { get; set; }=new List<Customer>();
        public List<WeedHackers_Data.Entities.Employee> AllEmployees { get; set; }=new List<WeedHackers_Data.Entities.Employee>();
        public List<Department> AllDepartments { get; set; }=new List<Department>();


    }
}