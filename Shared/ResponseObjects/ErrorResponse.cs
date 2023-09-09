using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ResponseObjects
{
    public class ErrorResponse
    {
        public List<string> Errors { get; private set; } = new();
        public bool IsShow { get; private set; }

        public ErrorResponse(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorResponse(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
