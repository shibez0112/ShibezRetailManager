
using SRMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRMDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}