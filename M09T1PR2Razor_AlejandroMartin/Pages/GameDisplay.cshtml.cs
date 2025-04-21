using M09T1PR2Razor_AlejandroMartin.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace M09T1PR2Razor_AlejandroMartin.Pages
{
    public class GameDisplayModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<GameDisplayModel> _logger;
        public List<GameDTO> GameList { get; set; } = new List<GameDTO>();
        public GameDisplayModel(ILogger<GameDisplayModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient("GameApi");
            try
            {
                var response = await client.GetAsync("api/Game");
                if (response == null || !response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error loading games");
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    GameList = JsonSerializer.Deserialize<List<GameDTO>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
