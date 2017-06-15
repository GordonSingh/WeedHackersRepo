using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data.ServiceProcess;

namespace DS32017_WeedHackers.Models.ServiceModels
{
    public class InspectionModel:SecondaryBaseModel
    {
        public ServiceRequestModel ServiceRequest { get; set; }
    }
}