using Api.Klinger.Controllers;
using AutoMapper;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Klinger.v2.Controllers
{
    [ApiVersion("2.0")]
    [Route("Api/v{version:ApiVersion}/Test")]
    public class TestController : MainController
    {
        public TestController(INotifier notifier, IMapper mapper, IUser appUser) : base(notifier, mapper, appUser)
        {
        }
        [HttpGet]
        public string Value()
        {
            return "Sou v2";
        }
    }
}
