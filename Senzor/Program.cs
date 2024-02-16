using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Senzor
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            // Base URL of your local API
            string baseUrl = "http://localhost:5171";

            // Endpoint to send data to
            string endpoint = "/api/Values";

            string id = Console.ReadLine();

            int val = 20;
            DateTime vreme;
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int promena = rnd.Next(20)+1;
                val+= promena > 10? promena/2: -promena ;
                if (val < 0) val = 0;
                
   

                vreme = DateTime.Now;

                //  data to send
                var data = new
                {
                    value = val+i,
                    time = vreme,
                    idSenzora = id,
                
                };

                try
                {
                    // Serialize data to JSON
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                    // Create HttpClient instance
                    using (var httpClient = new HttpClient())
                    {
                        // Set base address of API
                        httpClient.BaseAddress = new Uri(baseUrl);

                        // Set JSON content type
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        // Create StringContent with JSON data
                        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                        // Send POST request
                        var response = await httpClient.PostAsync(endpoint, content);

                        // Check if request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Data value: {val+i} time: {vreme} sent successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to send data. Status code: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                Thread.Sleep(2000);
            }
        }
    }
 }
