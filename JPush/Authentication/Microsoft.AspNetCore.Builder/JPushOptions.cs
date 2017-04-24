using System;
using System.Collections.Generic;
using System.Text;
using JPush.Authentication;

namespace Microsoft.AspNetCore.Builder
{
    public class JPushOptions: OAuthOptions
    {
        public JPushOptions() {
            AuthenticationScheme = JPushAuthenticationDefaults.AuthenticationScheme;
            DisplayName = JPushAuthenticationDefaults.DisplayName;
            ClaimsIssuer = JPushAuthenticationDefaults.Issuer;

            AuthorizationEndpoint = JPushAuthenticationDefaults.AuthorizationEndpoint;

            Scope.Add("profile");
        }

    }
}
