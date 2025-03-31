using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Students.Landing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Логин по имени пользователя/паролю (получаем access_token)
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var client = _httpClientFactory.CreateClient();

            // Достаём настройки для клиента входа (students-api)
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", _config["Keycloak:ClientId"]! },
                { "client_secret", _config["Keycloak:ClientSecret"]! },
                { "username", model.Username },
                { "password", model.Password }
            };

            // Шлём запрос в Keycloak
            var response = await client.PostAsync(
                _config["Keycloak:TokenUrl"],
                new FormUrlEncodedContent(parameters));

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return Unauthorized($"Неверный логин или пароль. Детали: {err}");
            }

            var content = await response.Content.ReadAsStringAsync();
            // Возвращаем JSON с access_token/refresh_token
            return Ok(JsonDocument.Parse(content));
        }

        /// <summary>
        /// Админ-регистрация пользователя (создаёт user в Keycloak через сервис-аккаунт admin-api)
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var client = _httpClientFactory.CreateClient();

            // 1) Получаем admin token (client_credentials flow) для admin-api
            var tokenRequest = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _config["Keycloak:AdminClientId"]! },
                { "client_secret", _config["Keycloak:AdminClientSecret"]! }
            };

            var tokenResponse = await client.PostAsync(
                _config["Keycloak:TokenUrl"],
                new FormUrlEncodedContent(tokenRequest));

            if (!tokenResponse.IsSuccessStatusCode)
            {
                var err = await tokenResponse.Content.ReadAsStringAsync();
                return Unauthorized($"Не удалось получить admin токен. Детали: {err}");
            }

            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenJson = JsonDocument.Parse(tokenContent);
            var accessToken = tokenJson.RootElement.GetProperty("access_token").GetString();

            // 2) Создаём пользователя через Admin API
            var user = new
            {
                username = model.Email,
                email = model.Email,
                firstName = model.FirstName,
                lastName = model.LastName,
                enabled = true,
                credentials = new[]
                {
                    new {
                        type = "password",
                        value = model.Password,
                        temporary = false
                    }
                }
            };

            var userJson = JsonSerializer.Serialize(user);
            var requestContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var userCreateResponse = await client.PostAsync(
                $"{_config["Keycloak:AdminUrl"]}/users",
                requestContent);

            if (!userCreateResponse.IsSuccessStatusCode)
            {
                var err = await userCreateResponse.Content.ReadAsStringAsync();
                return StatusCode((int)userCreateResponse.StatusCode, $"Ошибка при создании пользователя: {err}");
            }

            return Ok("Пользователь успешно зарегистрирован");
        }
    }

    public record LoginRequest(string Username, string Password);
    public record RegisterRequest(string Email, string FirstName, string LastName, string Password);
}
