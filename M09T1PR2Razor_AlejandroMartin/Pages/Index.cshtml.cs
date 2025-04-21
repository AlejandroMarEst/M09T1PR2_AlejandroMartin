using M09T1PR2Razor_AlejandroMartin.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace M09T1PR2Razor_AlejandroMartin.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public List<GameDTO> Games { get; set; } = new List<GameDTO>();
    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
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
                _logger.LogError("Error loading Games");
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                Games = JsonSerializer.Deserialize<List<GameDTO>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
    public IActionResult OnPostGameInfo(int id)
    {
        return RedirectToPage("GameInfo", new { id = id });
    }
}
