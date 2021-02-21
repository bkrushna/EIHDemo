using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace EIHTestPortal.Models
{
    public class ErrorModel
    {
        public HttpStatusCode Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}