using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace M09T1PR2Razor_AlejandroMartin.Pages
{
    public class ChatModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;

        public bool LoggedIn { get; set; } = false;
        public string UserName { get; set; }

        public ChatModel(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGet()
        {
            LoggedIn = Tools.TokenHelper.IsTokenSession(HttpContext.Session.GetString("AuthToken"));
            if (!LoggedIn)
            {
                GoIndex();
            }
            else
            {
                var client = _httpClient.CreateClient("GameApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
                var response = await client.GetAsync("api/Auth/GetName");
                UserName = await response.Content.ReadAsStringAsync();
            }
        }

        private IActionResult GoIndex()
        {
            return RedirectToPage("index");
        }
    }
}
