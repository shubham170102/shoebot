 
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
﻿
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;


namespace ConsoleApp3
{
    class Program
    {
        static int ya = 0;
        static async Task Main(string[] args)
        {
           
            var any = true;
            var size = "8";
            var website = "nrml.ca";
            var category = "/collections/footwear";
            var many = 5000;
            string Response = "";
            string Keyword = "CDG";
            Uri site2020 = new Uri("https://" + website + category + "/products.json?limit=" + many);
            HttpWebRequest request10 = (HttpWebRequest)WebRequest.Create(site2020);
            request10.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36";

            using (var response = await request10.GetResponseAsync())
            using (Stream streamResponse = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(streamResponse))
            {
                Response = await streamReader.ReadToEndAsync();
            }
            var n1 = 0;
            int stop = 0;
            var details = JObject.Parse(Response);
            foreach (var items in details)
            {
                var ahhh = items.Value;
                var items2 = ahhh.ToString();
                JArray data = JArray.Parse(items2);
                var icount = data.Count;
                
                    while (n1 != icount)
                    {
                      
                        var data2 = data[n1];
                        if (data2.ToString().Contains(Keyword) == true)
                        {
                            var details2 = JObject.Parse(data2.ToString());
                            foreach (var items3 in details2)
                            {
                                if (items3.Key == "variants")
                                {
                                    JArray data23 = JArray.Parse(items3.Value.ToString());
                                    var icount2 = data23.Count;
                                    var n2 = 0;
                                    while (n2 != icount2)
                                    {
                                        var data24 = data23[n2];
                                        var job4 = JObject.Parse(data24.ToString());
                                        if (any == false)
                                        {
                                            if (job4.Value<string>("option1") == size)
                                            {

                                                if (job4.Value<bool>("available") == false)
                                                {
                                                    Console.WriteLine("Out Of Stock");
                                                    stop = 1;

                                                }
                                                else
                                                {
                                                    Console.WriteLine("Found");
                                                    await BuyAsync(job4.Value<string>("id"));
                                                    stop = 1;


                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (job4.Value<bool>("available") == true)
                                            {
                                                Console.WriteLine("Found");
                                                await BuyAsync(job4.Value<string>("id"));
                                                stop = 1;
                                                break;
                                            }

                                        }

                                        n2++;

                                    }
                                }
                            }
                        }
                        n1++;
                        
                    
                }
               
            }
           

        }
        public static async Task BuyAsync(string id)
        {
            // Set a variable to the Documents path.
            Console.WriteLine("Adding To Cart!");
            int done12 = 0;
            //////////////////////////////////////////////////////////////////////////////////////////////////////Add to cart
            var parameters1 = new Dictionary<string, string> { { "id", id }, { "quantity", "1" } };
            CookieContainer cookies = new CookieContainer();



            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(parameters1));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new System.Net.Http.StringContent(stringPayload, Encoding.UTF8, "application/json");

            string lol = "";
            string limit1012 = "";
            string responsestring = "";
            string carts = "";

            string different = "";
            var httpClientHandler11 = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true,
                AllowAutoRedirect = true
            };
            using (var httpClient = new HttpClient(httpClientHandler11))
            {

                // Do the actual request and await the response


                httpClient.DefaultRequestHeaders.Add("origin", "https://nrml.ca/");

                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("x-requested-with", "XMLHttpRequest");

                var httpResponse = await httpClient.PostAsync("https://nrml.ca/cart/add.js", httpContent);
                httpResponse.Headers.GetValues("Set-Cookie").FirstOrDefault();
                responsestring = httpResponse.ToString();



                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    Console.WriteLine(responseContent);


                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }
            }

            ///////////////////////////////////////////Get the result
            ///

            Uri site = new Uri("https://nrml.ca/checkout.json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(site);
            request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";

            request.CookieContainer = cookies;


            
            WebResponse response1 = request.GetResponse();
            string lol102 = response1.ResponseUri.ToString();
            StreamReader sr = new StreamReader(response1.GetResponseStream());
            var source = sr.ReadToEnd();

            Uri site32 = new Uri(lol102);
            HttpWebRequest request11 = (HttpWebRequest)WebRequest.Create(site32);
            request11.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";


            request11.CookieContainer = cookies;

            
            WebResponse response111 = request11.GetResponse();

            StreamReader sr21 = new StreamReader(response111.GetResponseStream());
            var source11 = sr21.ReadToEnd();

            //Console.WriteLine(source11);


            string idk4 = response1.Headers["Set-Cookie"];
            /////////////////////////request response
           




            Console.WriteLine(cookies.GetCookieHeader(site));
            string data3 = getBetween(source11, "authenticity_token", "/>");
            string data4 = data3.Trim('"');
            data4 = data4.Replace("value=", "");
            data4 = data4.Replace('"', '@');
            data4 = getBetween(data4, "@", "@");



            string data1 = getBetween(source11, "Shopify.Checkout.token", "Shopify.Checkout.currency");
            string data105 = data1.Trim(new Char[] { ' ', '"', '=' });
            string data106 = data105.Substring(0, data105.Length - 3);









            Console.WriteLine("Submitting Info!");




            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            Uri site11 = new Uri("https://nrml.ca/cart/shipping_rates.json?shipping_address[zip]=J4W3J2&shipping_address[country]=CANADA&shipping_address[province]=QC");
            HttpWebRequest request1122 = (HttpWebRequest)WebRequest.Create(site11);
            request1122.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";

            request1122.CookieContainer = cookies;


          
            WebResponse response1111 = request1122.GetResponse();

            StreamReader sr11 = new StreamReader(response1111.GetResponseStream());
            string source112 = sr11.ReadToEnd();

            string text = source112;
            string price = "";
            //////////name
            source112 = getBetween(text, "name", "presentment");
            source112 = source112.Trim('"');
            source112 = source112.Trim(':');
            source112 = source112.Trim(',');
            source112 = source112.Replace('"', '@');
            source112 = source112.Replace("@", "");
            source112 = source112.Replace(" ", "%20");
            //////////price
            price = text.Replace('"', '@');
            price = getBetween(price, "@price@", ",");
            price = getBetween(price, "@", "@");


            //////////////////shipping id
            ///
            string shipping = "" + "shopify-" + source112 + "-" + price;


            Console.WriteLine(shipping);


            string gateway = "";
            var httpClientHandler12 = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true,
                AllowAutoRedirect = true
            };
            // WaitForSecond(20);
            ////Captcha

            using (var httpClient12 = new HttpClient(httpClientHandler12))
            {

                // Do the actual request and await the response




                ///////
                ///
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("checkout[shipping_address][first_name]", "Shoe"),
                    new KeyValuePair<string, string>("checkout[shipping_address][last_name]", "Bot"),
                    new KeyValuePair<string, string>("authenticity_token", data106),
                    new KeyValuePair<string, string>("commit", "Continue"),
                    new KeyValuePair<string, string>("_method", "patch"),
                    new KeyValuePair<string, string>("previous_step", "contact_information"),
                    new KeyValuePair<string, string>("checkout[email]", "shoebot0@gmail.com"),
                    new KeyValuePair<string, string>("checkout[shipping_address][phone]", "450-443-0358"),
                    new KeyValuePair<string, string>("checkout[shipping_address][address1]", "Continue"),
                    new KeyValuePair<string, string>("checkout[shipping_address][address1]", "6400 Boulevard Taschereau"),
                    new KeyValuePair<string, string>("checkout[shipping_address][country]", "CANADA"),
                    new KeyValuePair<string, string>("checkout[shipping_address][province]", "QC"),
                     new KeyValuePair<string, string>("g-recaptcha-response", ""),
                      new KeyValuePair<string, string>("checkout[shipping_address][city]", "Brossard"),
                      new KeyValuePair<string, string>("checkout[shipping_address][zip]", "J4W3J2"),
                      new KeyValuePair<string, string>("checkout[client_details][javascript_enabled]", "1"),
                      new KeyValuePair<string, string>("checkout[buyer_accepts_marketing]", "1")
                }



                );

                httpClient12.DefaultRequestHeaders.Add("origin", "https://pay.shopify.com");

                httpClient12.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");




                ///location



                var result = await httpClient12.PostAsync("https://nrml.ca/13343831/checkouts/" + data106, formContent);
                string responseContent = await result.Content.ReadAsStringAsync();

                gateway = getBetween(responseContent, "checkout_payment_gateway_", "\"");
                gateway = new String(gateway.Where(Char.IsDigit).ToArray());




                //Console.WriteLine(responseContent);


                // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net



                done12 = 1;






            }

            string total = "";

            string authentic = "";
            if (done12 == 1)
            {
                var httpClientHandler13 = new HttpClientHandler
                {
                    CookieContainer = cookies,
                    UseCookies = true,
                    AllowAutoRedirect = true
                };
                using (var httpClient12 = new HttpClient(httpClientHandler13))
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {

                      new KeyValuePair<string, string>("authenticity_token", data106),

                      new KeyValuePair<string, string>("_method", "patch"),
                      new KeyValuePair<string, string>("previous_step", "shipping_method"),
                      new KeyValuePair<string, string>("step", "payment_method"),
                      new KeyValuePair<string, string>("checkout[shipping_rate][id]", shipping),

                    }



                     );

                    httpClient12.DefaultRequestHeaders.Add("origin", "https://pay.shopify.com");

                    httpClient12.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");




                    ///location


                    Console.WriteLine("Submitting Shipping!");
                    var result = await httpClient12.PostAsync("https://nrml.ca/13343831/checkouts/" + data106, formContent);
                    string responseContent = await result.Content.ReadAsStringAsync();
                    Uri site321 = new Uri("https://nrml.ca/13343831/checkouts/" + data106);
                    HttpWebRequest request323 = (HttpWebRequest)WebRequest.Create(site321);
                    request323.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";


                    request323.CookieContainer = cookies;

                   
                    WebResponse response1121 = request323.GetResponse();

                    StreamReader sr219 = new StreamReader(response1121.GetResponseStream());
                    var source1187 = sr219.ReadToEnd();





                    total = getBetween(source1187, "checkout_total_price", ">");
                    total = new String(total.Where(Char.IsDigit).ToArray());

                    authentic = getBetween(source1187, "authenticity_token", ">");

                    authentic = authentic.Replace('"', '@');
                    authentic = authentic.Substring(authentic.IndexOf('@') + 1);
                    authentic = getBetween(authentic, "@", "@");
                    Console.WriteLine(total);

                    Console.WriteLine(gateway);

                    Console.WriteLine(authentic);
                    done12 = 2;



                }

            }
            string answer = "";
            if (done12 == 2)
            {
                Console.WriteLine("Generating Payment Token!");
                var parameters = new Dictionary<string, string> { { "number", "3411 111111 11111" } };
                parameters.Add("name", "Shoe Bot");
                parameters.Add("month", "09");
                parameters.Add("year", "2023");
                parameters.Add("verification_value", "932");
                parameters.Add("payment_session_scope", "nrml.ca");

                var stringPayload1 = await Task.Run(() => JsonConvert.SerializeObject(parameters));

                stringPayload1 = "{\"credit_card\":" + stringPayload1 + ",\"payment_session_scope\":\"" + "nrml.ca" + "\"}";
                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var httpContent1 = new System.Net.Http.StringContent(stringPayload1, Encoding.UTF8, "application/json");
                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class






                using (var httpClient = new HttpClient())
                {

                    // Do the actual request and await the response




                    ///////
                    ///





                    httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");



                    ///location



                    var result = await httpClient.PostAsync("https://elb.deposit.shopifycs.com/sessions", httpContent1);



                    string responseContent = await result.Content.ReadAsStringAsync();
                    responseContent = responseContent.Replace('"', '@');
                    responseContent = responseContent.Substring(responseContent.IndexOf(':') + 1);
                    responseContent = getBetween(responseContent, "@", "@");
                    answer = responseContent;
                    Console.WriteLine(responseContent);

                    done12 = 3;

                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net










                }
            }
            if (done12 == 3)
            {
                var httpClientHandler13 = new HttpClientHandler
                {
                    CookieContainer = cookies,
                    UseCookies = true,
                    AllowAutoRedirect = true
                };
                using (var httpClient12 = new HttpClient(httpClientHandler13))
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {

                      new KeyValuePair<string, string>("authenticity_token", authentic),

                      new KeyValuePair<string, string>("_method", "patch"),
                      new KeyValuePair<string, string>("previous_step", "payment_method"),
                      new KeyValuePair<string, string>("step", ""),
                      new KeyValuePair<string, string>("checkout[buyer_accepts_marketing]", "1"),
                      new KeyValuePair<string, string>("s", answer),
                      new KeyValuePair<string, string>("checkout[payment_gateway]", "75922051"),
                      new KeyValuePair<string, string>("checkout[credit_card][vault]", "false"),
                      new KeyValuePair<string, string>("checkout[different_billing_address]", "false"),
                      new KeyValuePair<string, string>("checkout[remember_me]", "false"),
                       new KeyValuePair<string, string>("checkout[vault_phone]", "+15646436423"),
                       new KeyValuePair<string, string>("checkout[total_price]", total),
                       new KeyValuePair<string, string>("complete", "1"),
                       new KeyValuePair<string, string>("checkout[client_details][browser_width]", "979"),
                       new KeyValuePair<string, string>("checkout[client_details][browser_height]", "631"),
                       new KeyValuePair<string, string>("checkout[client_details][javascript_enabled]", "1"),


                    }



                     );

                    httpClient12.DefaultRequestHeaders.Add("origin", "https://pay.shopify.com");

                    httpClient12.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");

                    Console.WriteLine("Submitting Payment!");

                    ///location



                    var result = await httpClient12.PostAsync("https://nrml.ca/13343831/checkouts/" + data106, formContent);
                    string responseContent = await result.Content.ReadAsStringAsync();


                    var result2 = await httpClient12.GetAsync("https://nrml.ca/13343831/checkouts/" + data106 + "/processing");
                    string responseContent2 = await result2.Content.ReadAsStringAsync();

                    Console.Clear();
                    Console.WriteLine(responseContent2);
                    Console.WriteLine("Processing!");
                    while (responseContent2.Contains("Processing") == true)
                    {
                        await Task.Delay(2000);
                        var result3 = await httpClient12.GetAsync("https://nrml.ca/13343831/checkouts/" + data106 + "/processing");
                        responseContent2 = await result3.Content.ReadAsStringAsync();
                        if (responseContent2.Contains("Card was declined") == true)
                        {
                            Console.WriteLine("Card was declined");
                            Environment.Exit(0);
                            break;
                        }
                        else if (responseContent2.Contains("Error") == true)
                        {
                            Console.WriteLine("Error");
                            Environment.Exit(0);
                            break;
                        }
                        else if (responseContent2.Length == 0)
                        {
                            Console.WriteLine("Error");
                            Environment.Exit(0);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Success");
                            Environment.Exit(0);
                            break;
                        }
                    }
                    Environment.Exit(0);


                }

            }
        }
       



        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

    }
   
    

}
