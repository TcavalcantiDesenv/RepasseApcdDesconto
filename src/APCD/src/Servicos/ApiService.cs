using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Servicos
{
    public class ApiService
    {
            private readonly string _vannonUrl = ConfigurationManager.AppSettings["SantaFarmaApiUrl"];
            private readonly string _vannonToken = ConfigurationManager.AppSettings["SantaFarmaApiToken"];

            public HttpClient CriarHttpClient(string urlComplement)
            {
                // var client = new HttpClient { BaseAddress = new Uri(_vannonUrl + urlComplement)};
                var client = new HttpClient { BaseAddress = new Uri(_vannonUrl + urlComplement) };
                //  client.DefaultRequestHeaders.Add("Authorization", $"ApiKey dan: {_vannonToken}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }

        }
    }