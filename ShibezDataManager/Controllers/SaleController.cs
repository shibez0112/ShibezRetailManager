﻿using Microsoft.AspNet.Identity;
using SRMDataManager.Library.DataAccess;
using SRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShibezDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {

        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData(); 
            string userId = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userId);
        }

        [Route("GetSaleReport")]
        public List<SaleReportModel> GetSaleReport()
        {
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }

        //public List<ProductModel> Get()
        //{
        //    ProductData data = new ProductData();

        //    return data.GetProducts();
        //}
    }
}
