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
    [Authorize(Roles ="Cashier,Admin")]
    public class ProductController : ApiController
    {
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();

            return data.GetProducts();
        }
    }
}
