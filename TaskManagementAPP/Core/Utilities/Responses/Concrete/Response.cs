using Core.Utilities.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities
{
    public class Response : IResponse
    {
        public bool Success { get; }

        public int StatusCode { get; }

        public Response(bool success,int statuscode)
        {
            Success = success;
            StatusCode = statuscode;
        }

        public Response(bool success)
        {
            Success = success;
        }
    }
}
