using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashewDocumentParser.Web.Middlewares
{
    public class AccessToken
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }

    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var authenticationCookieName = "access_token";
            var accessToken = context.Request.Cookies[authenticationCookieName];
           if (accessToken != null)
            {
                context.Request.Headers.Append("Authorization", "Bearer " + accessToken);
            }

            await _next.Invoke(context);
        }
    }
}
