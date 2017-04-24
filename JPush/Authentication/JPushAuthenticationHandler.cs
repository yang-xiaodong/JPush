using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace JPush.Authentication
{
    public class JPushAuthenticationHandler : OAuthHandler<JPushAuthenticationOptions>
    {

        public JPushAuthenticationHandler(HttpClient backchannel) : base(backchannel) {

        }


        protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(string code, string redirectUri) {

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.ClientId}:{Options.ClientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, Options.TokenEndpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                ["grant_type"] = "authorization_code",
                ["redirect_uri"] = redirectUri,
                ["code"] = code
            });

            var response = await Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, Context.RequestAborted);
            if (!response.IsSuccessStatusCode) {
                Logger.LogError("An error occurred while retrieving an access token: the remote server " +
                                "returned a {Status} response with the following payload: {Headers} {Body}.",
                                /* Status: */ response.StatusCode,
                                /* Headers: */ response.Headers.ToString(),
                                /* Body: */ await response.Content.ReadAsStringAsync());

                return OAuthTokenResponse.Failed(new Exception("An error occurred while retrieving an access token."));
            }

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            return OAuthTokenResponse.Success(payload);
        }

    }
}
