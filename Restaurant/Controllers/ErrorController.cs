using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Restaurant.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found :(";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;

                    // LogWarning() method logs the message under warning category
                    logger.LogWarning($"404 error occured. Path = {statusCodeResult.OriginalPath} and QueryString = "
                                    + $"{statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFoundError");
        }

        [Route("Error")]
        public ViewResult ExceptionErrorHandler()
        {
            // Retrieve the exception Details
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            // LogError() method logs the exceptin under Error category in the log
            logger.LogError($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");

            return View("Error");
        }
    }
}