﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace moneyverse.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("Account/VerifyCode")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}