using System.Collections.Generic;
using System.Linq;
using WeedHackers_Data;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Models.ApplicationModels
{
    public class CustomerRegistrationModel:BaseModel
    {
        public User RegistringUser { get; set; }
        public  Customer RegistringCustomer { get; set; }

        public List<CustomerType> CustomerTypes { get; set; }
        
        public CustomerRegistrationModel()
        {
            using (WeedHackersContext db=new WeedHackersContext())
            {
                CustomerTypes =db.CustomerTypes.ToList();
            }
            
        }

    }
}