using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Web_Api_Code_Book.Common.DTOs
{
    public class Response
    {
        public required int StatusCode { get; set; }
        public  required string StatusMessage { get; set; }
        public object? Payload { get; set; }
    }
}
