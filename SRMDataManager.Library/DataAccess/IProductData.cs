using SRMDataManager.Library.Models;
using System.Collections.Generic;

namespace SRMDataManager.Library.DataAccess
{
    public interface IProductData
    {
        ProductModel GetProductById(int ProductId);
        List<ProductModel> GetProducts();
    }
}