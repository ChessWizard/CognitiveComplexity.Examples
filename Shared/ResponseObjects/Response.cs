using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.ResponseObjects
{
    public class Response<TData>
    {
        public TData Data { get; set; }

        public int HttpStatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        public ErrorResponse ErrorDto { get; set; }

        public static Response<TData> Success(TData data, int statusCode)
        {
            return new Response<TData> { Data = data, HttpStatusCode = statusCode };
        }

        public static Response<TData> Success(int statusCode)
        {
            return new Response<TData> { Data = default, HttpStatusCode = statusCode };
        }

        public static Response<TData> Error(ErrorResponse errorDto, int statusCode)
        {
            return new Response<TData> { ErrorDto = errorDto, HttpStatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<TData> Error(string errorMessage, int statusCode, bool isShow = true)
        {
            return new Response<TData>
            {
                ErrorDto = new(errorMessage, isShow),
                HttpStatusCode = statusCode,
                IsSuccessful = false
            };
        }
    }
}

