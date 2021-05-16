using Api.Klinger.Controllers;
using AutoMapper;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        public TestController(INotifier notifier, IMapper mapper, IUser appUser, ILogger<TestController> logger) : base(notifier, mapper, appUser)
        {
            _logger = logger;
        }
        [HttpGet]
        public string Value()
        {
            int z = 0;

            double r = 45 / z;

            _logger.LogTrace("Log de Trace");
            _logger.LogDebug("Log de Debug");
            _logger.LogInformation("Log de informação");
            _logger.LogWarning("Log de Aviso");
            _logger.LogError("Log de Error");
            _logger.LogCritical("Log de Problema Critico");


            return "sou v2";
        }
    }
}
