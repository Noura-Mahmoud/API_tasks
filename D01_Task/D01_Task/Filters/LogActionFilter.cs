using D01_Task.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace D01_Task.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public static int counter = 0;
        private readonly ILogger<CarController> logger;
        public LogActionFilter(ILogger<CarController> logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
            counter++;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
      //      counter++;
        }
        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
            logger.LogInformation($"Received requests Until Now {counter}");
            logger.LogInformation($"Controller Name {controllerName}");
            logger.LogInformation($"Action Name {actionName}");
        }
    }
}
