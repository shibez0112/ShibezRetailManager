using SRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SRMDesktopUI.Library.Api
{
    public class SaleEndpoint : ISaleEndpoint
    {
        private IAPIHelper _apiHelper;
        public SaleEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;

        }

        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage respone = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (respone.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

    }
}
