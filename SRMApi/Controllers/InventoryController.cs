using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRMDataManager.Library.DataAccess;
using SRMDataManager.Library.Models;

namespace SRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IInventoryData _inventoryData;

        public InventoryController(IConfiguration config, IInventoryData inventoryData)
        {
            _config = config;
            _inventoryData = inventoryData;
        }
        // or relationship
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet]
        public List<InventoryModel> Get()
        {
            return _inventoryData.GetInventory();
        }

        // and relationship
        [Authorize(Roles = "WarehouseWorker")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post(InventoryModel item)
        {
            _inventoryData.SaveInventoryRecord(item);
        }
    }
}
