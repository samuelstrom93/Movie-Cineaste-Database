using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;


//Referenser: 
//https://www.youtube.com/watch?v=82ZYWnnp-zk (video nr 1)
// https://www.youtube.com/watch?v=jeBttUIqpuc (video nr2)
//https://www.youtube.com/watch?v=LSkbnpjCEkk (video nr3)

namespace CMDbAPI.Controllers
{
    public class ErrorController : Controller
    {

        //Referens:(video nr 1) & (video nr2)
        //OBS!! Använd EJ StackTrace i vyn, det är känslig info!!
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //Lagrar dessa 3 viewbags i vyn
            ViewBag.exceptionPath = exceptionHandler.Path;
            ViewBag.exceptionMessage = exceptionHandler.Error.Message;
            ViewBag.StackTrace = exceptionHandler.Error.StackTrace;

            return View("Error");
        }





        //Referens:  (video nr2)
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 400:
                    ViewBag.ErrorMessage = "Something went wrong with your request. Please try again later.";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
                case 404: 
                    ViewBag.ErrorMessage = "The page could not be found. Our staff is working to fix the problem.";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
                case 500: 
                    ViewBag.ErrorMessage = "The connection to the server is broken, we are working to fix the problem. Please try again later.";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
                case 502: 
                    ViewBag.ErrorMessage = "Service offline, please try again later. Our staff is working to fix the problem.";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
                default:
                    ViewBag.ErrorMessage = "Something went wrong with your request, please try again later. Our staff is looking in to the problem.";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
                    
            }
            return View("Error");
        }



    }
}
