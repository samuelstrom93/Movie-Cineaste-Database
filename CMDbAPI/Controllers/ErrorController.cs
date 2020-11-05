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
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.exceptionPath = exceptionHandler.Path;
            ViewBag.exceptionMessage = exceptionHandler.Error.Message;

            return View("Error");
        }
    }
}
