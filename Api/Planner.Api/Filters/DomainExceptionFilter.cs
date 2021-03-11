using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Planner.Application.Exceptions;
using Planner.Domain.Exceptions;
using Planner.Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace Planner.Api.Filters
{
    public sealed class DomainExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            DomainException domainException = context.Exception as DomainException;
            if (domainException != null)
            {
                string json = JsonSerializer.Serialize(domainException.Message);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            ApplicationException applicationException = context.Exception as ApplicationException;
            if (applicationException != null)
            {
                string json = JsonSerializer.Serialize(applicationException.Message);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            InfrastructureException infrastructureException = context.Exception as InfrastructureException;
            if (infrastructureException != null)
            {
                string json = JsonSerializer.Serialize(infrastructureException.Message);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
