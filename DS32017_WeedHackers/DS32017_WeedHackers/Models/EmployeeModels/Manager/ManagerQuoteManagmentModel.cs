using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data.ServiceProcess;

namespace DS32017_WeedHackers.Models.EmployeeModels.Manager
{
    public class ManagerQuoteManagmentModel:SecondaryBaseModel
    {
        public ServiceRequest ServiceRequest { get; set; }
        public int [] AssignedEmployeeIds { get; set; }
        public List<WeedHackers_Data.Entities.Employee> Employees { get; set; }
        
    }
}