using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TrueLayerTestApp
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static int numberofposts = 0;
        static void Main(string[] args)
        {

            //check both arugments are provided
            if (args.Length !=2 )
            {
                System.Console.WriteLine("Please enter --posts and number of posts arguments, only two arguments are required");
                return;
            }
            else
            {
                if (args[0].ToLower() != "--posts")
                {
                    System.Console.WriteLine("Invalid first argument, Please enter --posts as a first argument");
                    return;
                }
                if (int.TryParse(args[1], out numberofposts))
                {
                    //check for number of posts can be positive number between 1 and 100
                    if (numberofposts > 100 && numberofposts < 1)
                    {
                        System.Console.WriteLine("Invalid argument, number of posts can be only between 1 and 100");
                        return;
                    }
                    else
                    {
                        
                        System.Console.WriteLine(RunAsync().GetAwaiter().GetResult());
                        Console.ReadLine();
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid second argument for number of posts, Please enter  number of posts which can be between 1 and 100");
                    return;
                }

            }
        }

        /// <summary>
        /// call the webapi to read the data asynchornously
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static async Task<string> GetNewsDataAsync(string path)
        {
            string responseData = string.Empty;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                responseData = await response.Content.ReadAsStringAsync();
            }
            response.Dispose();
            return responseData;
        }
        static async Task<string> RunAsync()
        {
            string responseData = string.Empty;
            // Update port # in the following line as required, currentlu this is based on my setup
            client.BaseAddress = new Uri(string.Format("https://localhost:44324/"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                // Get the news data from the REST API
                responseData = await GetNewsDataAsync(client.BaseAddress + "api/HackerNewsData/" + numberofposts);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return responseData;
        }
    }
}
