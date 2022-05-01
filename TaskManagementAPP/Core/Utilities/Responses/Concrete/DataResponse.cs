using Core.Utilities.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Responses.Concrete
{
    public class DataResponse<T> : IDataResponse<T>
    {
        public bool Success { get; } = true;
        public T Data { get; }

        public int StatusCode { get; }
        public string Message { get; set; }
        public DataResponse(T data, int statuscode)
        {
            Data = data;
            StatusCode = statuscode;
        }
        public DataResponse(T data, int statuscode,string message)
        {
            Data = data;
            StatusCode = statuscode;
            Message = message;
        }
    }
}
