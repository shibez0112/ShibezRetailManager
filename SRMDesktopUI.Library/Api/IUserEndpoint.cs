using SRMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRMDesktopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
    }
}