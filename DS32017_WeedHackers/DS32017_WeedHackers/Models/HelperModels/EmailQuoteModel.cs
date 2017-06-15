using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS32017_WeedHackers.Models.HelperModels
{
    public class EmailQuoteModel
    {
        public EmailFormModel EmailFormModel { get; set; }

        public string QuotationNumber { get; set; }
        public string ServiceType { get; set; }
        public string UnitPrice { get; set; }
        public string UnitQuantity { get; set; }
        public string UnitSubtotal { get; set; }
        public string UnitTotal { get; set; }
        public string Email { get; set; }
        public string Tell { get; set; }
    }
}