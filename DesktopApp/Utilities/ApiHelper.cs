using System.Net.Http;
using System.Text;
using System.Windows;

namespace DesktopApp
{
    class ApiHelper
    {
        public static async Task<string> SendRequest(string baseUrl, HttpMethod method, string endpoint, string? jsonPayload = null)
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(baseUrl);

            // Create HttpRequestMessage
            var request = new HttpRequestMessage(method, endpoint);
            

            if (!string.IsNullOrEmpty(GetToken()))
            {
                request.Headers.Add("Authorization", "Bearer " + GetToken());
            }

            // Add payload for POST and PUT requests
            if (method == HttpMethod.Post || method == HttpMethod.Put )
            {
                request.Content = new StringContent(jsonPayload!, Encoding.UTF8, "application/json");
            }
           
            // Send request
            var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();

            // Process response
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                return $"Request failed with status code {response.StatusCode}";
            }
        }
        
        private static string GetToken()
        {
            string token = string.Empty;
            try
            {
                if (((App)Application.Current) != null)
                {
                    token = ((App)Application.Current).AccessToken;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return token;
        }
    }
}
