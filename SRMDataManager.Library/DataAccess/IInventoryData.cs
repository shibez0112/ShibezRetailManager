using SRMDataManager.Library.Models;
using System.Collections.Generic;

namespace SRMDataManager.Library.DataAccess
{
    public interface IInventoryData
    {
        List<InventoryModel> GetInventory();
        void SaveInventoryRecord(InventoryModel item);
    }
}