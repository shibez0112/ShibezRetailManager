using Microsoft.AspNet.Identity;
using SRMDataManager.Library.DataAccess;
using SRMDataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace ShibezDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        // GET: User/Details/5
        [HttpGet]
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            UserData data = new UserData();
            
            return data.GetUserById(userId).First();

        }
    }
}
