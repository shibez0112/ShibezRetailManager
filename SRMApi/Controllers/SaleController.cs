using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRMDataManager.Library.DataAccess;
using SRMDataManager.Library.Models;
using System.Security.Claims;

namespace SRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleData _saleData;

        public SaleController(ISaleData saleData)
        {
            _saleData = saleData;
        }

        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); /*old way RequestContext.Principal.Identity.GetUserId();*/

            _saleData.SaveSale(sale, userId);
        }

        [Authorize(Roles = "Admin")]
        [Route("GetSaleReport")]
        [HttpGet]
        public List<SaleReportModel> GetSaleReport()
        {
            return _saleData.GetSaleReport();
        }
    }
}
