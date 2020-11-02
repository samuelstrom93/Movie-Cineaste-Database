using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace CMDbAPI.Controllers
{
    public class ErrorController : Controller
    {
        //Referens:https://www.youtube.com/watch?v=82ZYWnnp-zk
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //Lagrar dessa 3 viewbags i vyn
            ViewBag.exceptionPath = exceptionHandler.Path;
            ViewBag.exceptionMessage = exceptionHandler.Error.Message;
            ViewBag.exceptionStackTrace = exceptionHandler.Error.StackTrace;

            return View("Error");
        }
    }
}
