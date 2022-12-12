using SRMDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace SRMDesktopUI.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}