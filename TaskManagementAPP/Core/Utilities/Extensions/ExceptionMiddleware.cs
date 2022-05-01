using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Exceptions;
using Core.Utilities.Responses.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private readonly LoggerServiceBase _loggerServiceBase;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(typeof(PostgreSqlLogger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }
        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";
            if (e.GetType() == typeof(ValidationException))
            {
                IEnumerable<ValidationFailure> errors;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400;
                var validationerror = JsonConvert.SerializeObject(new ErrorResponse(400, errors.Select(x => x.ErrorMessage).ToList()), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                return httpContext.Response.WriteAsync(validationerror);
            }
            
            else if (e.InnerException is ApiException || e.GetType() == typeof(ApiException))
            {
                var ex = e.InnerException != null ? (ApiException)e.InnerException : (ApiException)e;
                httpContext.Response.StatusCode = ex.StatusCode;
                var apierror = JsonConvert.SerializeObject(new ErrorResponse(ex.StatusCode, ex.Errors),new JsonSerializerSettings { ContractResolver=new CamelCasePropertyNamesContractResolver()});
                return httpContext.Response.WriteAsync(apierror);
            }

            List<string> exceptions = new List<string>();
            
            if (e.InnerException != null)
            {
                exceptions.Add(e.InnerException.ToString());
                if (e.InnerException.Message != null)
                {
                    exceptions.Add(e.InnerException.Message);
                }
                else if (e.InnerException.InnerException.Message != null)
                {
                    exceptions.Add(e.InnerException.InnerException.Message);
                }
            }
            
            else if (e.Message != null)
            {
                exceptions.Add(e.Message);
            }
            var errorlogDetail = new ErrorLog
            {
                Errors = exceptions,
            };
            var serializederror=JsonConvert.SerializeObject(errorlogDetail);
            _loggerServiceBase.Error(serializederror);

            var error = JsonConvert.SerializeObject(new ErrorResponse(httpContext.Response.StatusCode, message));
            return httpContext.Response.WriteAsync(error);
        }
    }
}
