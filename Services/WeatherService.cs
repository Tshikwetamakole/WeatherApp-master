namespace WeatherAppp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string API_KEY = "c6316e61cebe0d89c2d058cd83420ff3";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherModel.WeatherResponse> GetWeatherByCoordinates(double latitude, double longitude)
        {
            // latitude = -23.883669;
            //longitude = 29.707878;
            var response = await _httpClient.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={API_KEY}&units=metric");

            if(response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<WeatherModel.WeatherResponse>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception("Failed to get weather data");
        }

        public async Task<WeatherModel.WeatherResponse> GetWeatherByCity(string city)
        {
            var response = await _httpClient.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}&units=metric");

            if(response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<WeatherModel.WeatherResponse>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception("Failed to get weather data");
        }
    }
}
