using ChuckNorrisJokesApp.Models;
using ChuckNorrisJokesApp.Services.Interfaces;
using System.Text.Json;

namespace ChuckNorrisJokesApp.Services
{
    public class DadJokeApiHttpService : IDadJokeApiService
    {
        private HttpClient _httpClient;
        private IConfigurationService _configurationService;

        public DadJokeApiHttpService(IConfigurationService service)
        {
            _configurationService = service;

            _httpClient = new HttpClient();
        }

        public async Task<string> GetDadJokeAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _configurationService.DadJokeApiUrl);

            request.Headers.Add(
                "Accept", "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not load Dad Joke");
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                DadJoke joke = JsonSerializer.Deserialize<DadJoke>(
                    jsonContent);
                if (joke == null || string.IsNullOrWhiteSpace(joke.joke))
                {
                    throw new Exception(
                        "Dad Joke data is invalid");
                }
                return joke.joke;
            }
            //string jsonContent = await response.Content.ReadAsStringAsync();

            //DadJoke joke = JsonSerializer.Deserialize<DadJoke>(
            //    jsonContent);

            //if (joke == null || string.IsNullOrWhiteSpace(joke.joke))
            //{
            //    throw new Exception(
            //        "Dad Joke data is invalid");
            //}

            return null;
        }
    }
}
