using System.Net;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace CliApi.Client
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {

            Console.WriteLine("Making the call.....");
            RunAsync().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static async Task RunAsync(){
            var config  = AuthConfig.ReadFromJsonFile("appsettings.json");
            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                    .WithClientSecret(config.ClientSecret)
                    .WithAuthority(new Uri(config.Authority))
                    .Build();
            
            var resourceIds = new string[] {config.ResourceID};

            AuthenticationResult result = null;
            try{
                result = await app
                .AcquireTokenForClient(resourceIds)
                .ExecuteAsync();
            
                if(!string.IsNullOrEmpty(result.AccessToken)){
                    var httpClient = new HttpClient();
                    var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                    if(defaultRequestHeaders.Accept == null || 
                    !defaultRequestHeaders.Accept.Any(m=> m.MediaType == "application/json")){

                       httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    }

                    defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
                    var response = await httpClient.GetAsync(config.BaseAddress);
                    if(response.IsSuccessStatusCode){
                        
                        string json = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(json);
                                   
                    }else{
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                        
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Content: {content}");
                    }

                    Console.ResetColor();     
                }                                
            }
            catch(MsalClientException ex){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.WriteLine("Execution Finished....");
        }
    }
}
