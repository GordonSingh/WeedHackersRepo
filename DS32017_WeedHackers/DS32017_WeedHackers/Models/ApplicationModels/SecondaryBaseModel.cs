using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS32017_WeedHackers.Models.ApplicationModels
{
    public class SecondaryBaseModel : BaseModel
    {
        public List<NavigationModel> SideNavModels { get; set; } = new List<NavigationModel>();
        
    }
}