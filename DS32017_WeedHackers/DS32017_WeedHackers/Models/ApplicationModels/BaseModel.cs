using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Models.ApplicationModels
{
    public class BaseModel
    {
        public User UserContext { get; set; }
        public List<NavigationModel> NavigationModels { get; set; }

        public BaseModel()
        {
           NavigationModels=new List<NavigationModel>();
        }

    }
}