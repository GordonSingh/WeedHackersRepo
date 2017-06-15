using System;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Models.Authentication
{
    public class SessionModel
    {
        public Guid Identifier { get; set; }
        public User User { get; set; }
        public DateTime ExpiryTime { get; set; }
        
    }
}