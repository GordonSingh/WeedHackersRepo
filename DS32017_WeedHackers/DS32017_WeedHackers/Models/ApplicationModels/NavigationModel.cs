using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS32017_WeedHackers.Models.ApplicationModels
{
    public class NavigationModel
    {
        public string Icon { get; set; } = "";
        public string Link { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
    }
}