using SRMDataManager.Library.Models;
using System.Collections.Generic;

namespace SRMDataManager.Library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string Id);
    }
}