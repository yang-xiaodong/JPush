using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JPush.Authentication
{
    public class JPushAuthenticationMiddleware : OAuthMiddleware<JPushAuthenticationOptions>
    {
        public JPushAuthenticationMiddleware(RequestDelegate next, 
            IDataProtectionProvider dataProtectionProvider,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder, 
            IOptions<SharedAuthenticationOptions> sharedOptions,
            IOptions<JPushAuthenticationOptions> options)
            : base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options) {
        }

        protected override AuthenticationHandler<JPushAuthenticationOptions> CreateHandler() {
            return base.CreateHandler();
        }
    }
}
