using SRMDesktopUI.Models;
using System.Threading.Tasks;

namespace SRMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}