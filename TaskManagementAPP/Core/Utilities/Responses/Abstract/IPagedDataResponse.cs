using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Responses.Abstract
{
    interface IPagedDataResponse<T> : IResponse
    {
         int TotalItems { get; }
         T Data { get; }
    }
}
