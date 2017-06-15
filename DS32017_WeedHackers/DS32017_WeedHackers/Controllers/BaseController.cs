using System.Web.Mvc;
using WeedHackers_Data;

namespace DS32017_WeedHackers.Controllers
{
    public abstract class BaseController:Controller
    {
        protected WeedHackersContext WeedHackersContext { get; private set; }

        protected BaseController()
        {
            WeedHackersContext = new WeedHackersContext();
        }

        protected override void Dispose(bool disposing)
        {
            WeedHackersContext.Dispose();
            base.Dispose(disposing);
        }
    }
}