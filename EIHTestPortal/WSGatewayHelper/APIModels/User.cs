using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIHTestPortal.WSGatewayHelper.APIModels
{
    /// <summary>
    /// this model is used for authenticating API
    /// , in production this will be handled at api gateway level
    /// </summary>
    public class User
    {
        public string UserId { get; set; }

        public string Password { get; set; }
    }
}