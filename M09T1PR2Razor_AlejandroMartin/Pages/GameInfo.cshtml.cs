using M09T1PR2Razor_AlejandroMartin.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace M09T1PR2Razor_AlejandroMartin.Pages
{
    public class GameInfoModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public GameDTO Game { get; set; } = new GameDTO();

        public GameInfoModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetGame(int id)
        {
            var client = _httpClientFactory.CreateClient("GameApi");
            try
            {
                var response = await client.GetAsync($"api/Game/{id}");
                if (response == null || !response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error loading Game Info");
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Game = JsonSerializer.Deserialize<GameDTO>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
