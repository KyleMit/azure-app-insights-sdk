using Azure.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using System;

namespace azure_app_insights_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.dev.json")
                .Build();
            var auth = config.GetSection("AppInsights").Get<AppInsightsAuth>();

            var aiClient = new AppInsightsClient(auth);

            aiClient.Query.Query()


        }
    }
}
