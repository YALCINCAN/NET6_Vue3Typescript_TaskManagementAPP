using Core.Utilities.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Responses.Concrete
{
    public class PagedDataResponse<T> : IPagedDataResponse<T>
    {
        public bool Success { get; } = true;
        public int TotalItems { get; }

        public T Data { get; }

        public int StatusCode { get; }

        public PagedDataResponse(T data, int statuscode, int totalitems)
        {
            Data = data;
            StatusCode = statuscode;
            TotalItems = totalitems;
        }
    }
}
