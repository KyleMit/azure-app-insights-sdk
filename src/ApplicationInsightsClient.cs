using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Azure.ApplicationInsights
{
    public class AppInsightsClient
    {
        private string _appId;
        private string _key;

        public AIMetrics Metrics { get; set; } = new AIMetrics();
        public AIEvents Events { get; set; } = new AIEvents();
        public AIQuery Query { get; set; } = new AIQuery();

        public AppInsightsClient(string appId, string key) { 
            _appId = appId;
            _key = key;
        }
        public AppInsightsClient(AppInsightsAuth authObj)
        {
            _appId = authObj.AppId;
            _key = authObj.Key;
        }

        public class AIMetrics
        {

        }
        public class AIEvents
        {

        }
        public class AIQuery
        {
            public void Query(string query, string applications, string timespan = null, string prefer = null)
            {
                var json = GetTelemetry
                ("DEMO_APP", "DEMO_KEY",
                    "metrics", "requests/duration", "interval=PT1H");
            }

            private const string URL = "https://api.applicationinsights.io/v1/apps/{0}/{1}/{2}?{3}";

            public static string GetTelemetry(string appid, string apikey,
                    string queryType, string queryPath, string parameterString)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-api-key", apikey);

                var req = string.Format(URL, appid, queryType, queryPath, parameterString);

                HttpResponseMessage response = client.GetAsync(req).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return response.ReasonPhrase;
                }
            }
        }

    }

    public class AppInsightsAuth
    {
        public string AppId { get; set; }
        public string Key { get; set; }
    }

}
