using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApp {
    class Program
    {
        static async Task Main(string[] args)
        {
            var any = true;
            var size = "8";
            var website = "nrml.ca";
            var category = "/collections/footwear";
            var many = 5000;
            string keyword = "CDG";
            string url = $"https://{website}{category}/products.json?limit={many}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36");
                
                try
                {
                    string response = await client.GetStringAsync(url);
                    JObject details = JObject.Parse(response);
                    
                    if (details != null && details["products"] is JArray items) // Ensure that 'products' is an array
                    {
                        foreach (JObject item in items)
                        {
                            JArray variants = item["variants"] as JArray;
                            if (variants != null)
                            {
                                foreach (JObject variant in variants)
                                {
                                    string option = variant.Value<string>("option1");
                                    bool available = variant.Value<bool>("available");

                                    if ((any || option == size) && available)
                                    {
                                        Console.WriteLine("Found");
                                        await BuyAsync(variant.Value<string>("id"));
                                        break; // Stop processing further if item is found and processed
                                    }
                                }
                            }
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request exception: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }
        }

        static async Task BuyAsync(string id)
        {
            // Implementation of BuyAsync method
            Console.WriteLine($"Buying ID: {id}");
            // Simulate some async operation
            await Task.Delay(1000);
        }
    }
}
