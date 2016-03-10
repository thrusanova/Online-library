namespace Online_library.Web.Controllers
{
    using Data;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;

    [ValidateInput(false)]
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && this.User.IsInRole("Administrator"));
            return isAdmin;
        }
    }
}