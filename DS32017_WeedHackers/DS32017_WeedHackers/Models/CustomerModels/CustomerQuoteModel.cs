using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data.Entities;
using WeedHackers_Data.ServiceProcess;

namespace DS32017_WeedHackers.Models.CustomerModels
{
    public class CustomerQuoteModel:SecondaryBaseModel
    {
        public List<ServiceRequest> CustomerQuotes { get; set; }=new List<ServiceRequest>();
       
    }
}