using SRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SRMDesktopUI.Library.Api
{
    public class UserEndpoint : IUserEndpoint
    {
        private IAPIHelper _apiHelper;
        public UserEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<UserModel>> GetAll()
        {
            using (HttpResponseMessage respone = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllUsers"))
            {
                if (respone.IsSuccessStatusCode)
                {
                    var result = await respone.Content.ReadAsAsync<List<UserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public async Task<Dictionary<string, string>> GetAllRoles()
        {
            using (HttpResponseMessage respone = await _apiHelper.ApiClient.GetAsync("api/User/Admin/GetAllRoles"))
            {
                if (respone.IsSuccessStatusCode)
                {
                    var result = await respone.Content.ReadAsAsync<Dictionary<string, string>>();
                    return result;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public async Task AddUserToRole(string userId, string roleName)
        {
            var data = new {userId, roleName };

            using (HttpResponseMessage respone = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/AddRole", data))
            {
                if (respone.IsSuccessStatusCode == false)
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public async Task RemoveUserFromRole(string userId, string roleName)
        {
            var data = new { userId, roleName };

            using (HttpResponseMessage respone = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/RemoveRole", data))
            {
                if (respone.IsSuccessStatusCode == false)
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }
    }
}
