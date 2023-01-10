using Microsoft.Extensions.Configuration;
using SRMDataManager.Library.Internal.DataAccess;
using SRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMDataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<UserModel> GetUserById(string Id)
        {
            var p = new { Id = Id };

            var output = _sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "SRMData");

            return output;
        }
    }
}
