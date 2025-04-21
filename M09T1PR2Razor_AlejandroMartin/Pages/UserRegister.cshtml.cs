using M09T1PR2Razor_AlejandroMartin.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M09T1PR2Razor_AlejandroMartin.Pages
{
    public class UserRegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<UserRegisterModel> _logger;
        [BindProperty]
        public UserRegisterDTO Register { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public UserRegisterModel(IHttpClientFactory httpClient, ILogger<UserRegisterModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            try
            {
                var client = _httpClient.CreateClient("GameApi");
                var response = await client.PostAsJsonAsync("api/Auth/register", Register);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Register susccesfull");
                    return RedirectToPage("/Index");
                }
                else
                {
                    _logger.LogInformation("Register failed");
                    ErrorMessage = "Registry error.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registry error");
                ErrorMessage = "There was an unexpected error. Try again.";
            }
            return Page();
        }
    }
}
