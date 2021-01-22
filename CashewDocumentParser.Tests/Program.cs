using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace CashewDocumentParser.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = MakeRequest();
            Console.WriteLine("Hello World!");
        }

        public static async Task<string> MakeRequest()
        {
            var client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b96af7aa360f455184efa22465f426d9");

            // Request parameters
            queryString["language"] = "en";
            queryString["detectOrientation"] = "true";
            var uri = "https://kyoceraformextractor.cognitiveservices.azure.com/vision/v3.1/ocr?" + queryString;

            // Request body
            string filePath = Path.Combine(Environment.CurrentDirectory, @"PDFs\", "Agility.jpg");
            byte[] byteData = File.ReadAllBytes(filePath);

            /*using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                response = await client.PostAsync(uri, content);
            }*/
            form.Add(new ByteArrayContent(byteData));
            var result = await client.PostAsync(uri, form);
            var response = await result.Content.ReadAsStringAsync();
            return response;
        }
    }
}
