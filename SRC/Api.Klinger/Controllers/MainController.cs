using AutoMapper;
using Business.Interfaces;
using Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Api.Klinger.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        protected readonly IMapper _mapper;
        protected MainController(INotifier notifier, IMapper mapper)
        {
            _notifier = notifier;
            _mapper = mapper;
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) NotifieErrorModelInvalid(modelState);

            return CustomResponse();
        }
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                return Ok(new { sucess = true, data = result });
            }                     

            return BadRequest(new
            {
                sucess = false,
                data = _notifier.FindAlls().Select(x=>x.Message)
            });
        }

        protected bool OperationValid()
        {
            return !_notifier.HasNotification();
        }

        protected void NotifieErrorModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(x => x.Errors);

            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                ErrorNotifier(erroMsg);
            }
        }
        protected void ErrorNotifier(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }

    
}
