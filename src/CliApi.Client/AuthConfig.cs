using System;
using System.IO;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace CliApi.Client
{
    public class AuthConfig
    {
        public string Instance {get;set;} = "https://login.microsoftonline.com/{0}";
        public string TenantId { get; set; }
        public string ClientId {get;set;}
        public string Authority {
            get{
                return string.Format(CultureInfo.InvariantCulture,Instance,TenantId);
            }
        }

        public string ClientSecret  {get;set;}
        public string BaseAddress {get;set;}
        public string ResourceID {get;set;}

        public AuthConfig()
        {
        
        }
        public static AuthConfig ReadFromJsonFile(string path){
            IConfiguration Configuration;
            
              var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
             
             var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";

               var builder = new ConfigurationBuilder();

            builder
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //only add secrets in development
            if (isDevelopment) 
            {
                builder.AddUserSecrets<Program>();
            }

            
            Configuration = builder.Build();            
            return Configuration.Get<AuthConfig>();
        }
    }
}