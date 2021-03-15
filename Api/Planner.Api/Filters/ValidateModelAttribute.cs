using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Planner.Api.Filters
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
