using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace Slacker.Services
{
    public class SlackService(HttpClient client) : ISlackService
    {
        private readonly string _token = "";

        public async Task UpdateSlackStatus(string text, string emoji)
        {
            var profile = new
            {
                profile = new
                {
                    status_text = text,
                    status_emoji = emoji,
                    status_expiration = 0
                }
            };

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            var response = await client.PostAsJsonAsync("https://slack.com/api/users.profile.set", profile);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error setting status: {response.StatusCode}");
            }
        }
    }
}