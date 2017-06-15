using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data.Entities;
using WeedHackers_Data.ServiceProcess;

namespace DS32017_WeedHackers.Models.ServiceModels
{
    public class ServiceRequestModel : SecondaryBaseModel
    {
        public List<Service> Services { get; set; } = new List<Service>();
        public ServiceRequest ServiceRequest { get; set; }=new ServiceRequest();
    }
}