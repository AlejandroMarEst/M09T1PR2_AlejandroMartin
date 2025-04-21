using M09T1PR2Razor_AlejandroMartin.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M09T1PR2Razor_AlejandroMartin.Pages
{
    public class UserLoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger _logger;
        [BindProperty]
        public UserLoginDTO Login { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public UserLoginModel(IHttpClientFactory httpClient, ILogger<UserLoginModel> logging)
        {
            _httpClient = httpClient;
            _logger = logging;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            try
            {
                var client = _httpClient.CreateClient("GameApi");
                var response = await client.PostAsJsonAsync("api/Auth/login", Login);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(token))
                    {
                        HttpContext.Session.SetString("AuthToken", token);
                        _logger.LogInformation("Login susccesfull");
                        return RedirectToPage("/Index");
                    }
                }
                else
                {
                    _logger.LogInformation("Login failed");
                    ErrorMessage = "Credencials incorrectes o accés no autoritzat.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durant el login");
                ErrorMessage = "Error inesperat. Torna-ho a intentar.";
            }
            return Page();
        }
    }
}
