using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DonM.Crowe.Infrastructure.Models;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace DonM.Crowe.ConsoleApp
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync(args).GetAwaiter().GetResult();
        }

        static async Task RunAsync(string[] args)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("greetingUrl"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var url = client.BaseAddress;

                if (args != null && args.Length > 0)
                {
                    // post all contents in args as a new greeting
                    foreach (string argName in args)
                    {
                        await PostGreetingAsync(url.PathAndQuery, new Greeting { Name = argName });
                    }
                }
                
                // Get the product
                IEnumerable<Greeting> greetingList = await GetGreetingAsync(url.PathAndQuery);
                ShowGreeting(greetingList);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }


        static async Task<IEnumerable<Greeting>> GetGreetingAsync(string path)
        {
            IEnumerable<Greeting> greetingGet = null;

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                greetingGet = await response.Content.ReadAsAsync<IEnumerable<Greeting>>();
            }
            return greetingGet;
        }

        static void ShowGreeting(IEnumerable<Greeting> greetingList)
        {
            foreach (Greeting greeting in greetingList)
            {
                Console.WriteLine($"{greeting.Name}");
            }
        }

        static async Task<Greeting> PostGreetingAsync(string path, Greeting greeting)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(path, greeting);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            greeting = await response.Content.ReadAsAsync<Greeting>();
            return greeting;
        }
    }
}

