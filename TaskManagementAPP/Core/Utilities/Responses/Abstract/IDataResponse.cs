using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Responses.Abstract
{
    public interface IDataResponse<T> : IResponse
    {
        T Data { get; }
        string Message { get; }
    }
}
