using ChuckNorrisJokesApp.Models;
using ChuckNorrisJokesApp.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ChuckNorrisJokesApp.Services
{
    public class ChuckNorrisApiHttpService : IChuckNorrisApiService
    {
        private HttpClient _httpClient;
        private IConfigurationService _configurationService;
        public ChuckNorrisApiHttpService(IConfigurationService service)
        {
            _configurationService = service;

            _httpClient = new HttpClient();
        }

        public async Task<string> GetJokeAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_configurationService.ChuckNorrisApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();

   

                 ChuckNorrisJoke joke = JsonSerializer.Deserialize<ChuckNorrisJoke>(jsonContent);

                if (joke != null)
                {
                    return joke.value;
                }            
            }

            return null;
        }
        public async Task<string> GetDadJokeAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _configurationService.DadJokeApiUrl);

            request.Headers.Add(
                "Accept", "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not load Dad Joke");
            }
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
    }
}