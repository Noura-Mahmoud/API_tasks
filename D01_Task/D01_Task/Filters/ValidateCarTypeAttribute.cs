using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using MVC_D02_Task2.Models;

namespace D01_Task.Filters
{
    public class ValidateCarTypeAttribute: ActionFilterAttribute
    {
        private readonly ILogger<ValidateCarTypeAttribute> _logger;
        private readonly IConfiguration _configuration;

        public ValidateCarTypeAttribute(
            ILogger<ValidateCarTypeAttribute> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogWarning("Filter execution started");
          //  var allowed = _configuration.GetValue<string>("AllowedLocations");
            Car? car = context.ActionArguments["car"] as Car;

            var regex = new Regex("^(Electric|Gas|Diesel|Hybrid|)$",
                RegexOptions.IgnoreCase,
                TimeSpan.FromSeconds(2));

            if (car is null || !regex.IsMatch(car.Type))
            {
                //Short Circuit with BadRequest
                context.ModelState.AddModelError("Type", "Type is not covered");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
