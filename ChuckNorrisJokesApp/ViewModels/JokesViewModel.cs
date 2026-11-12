using ChuckNorrisJokesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ChuckNorrisJokesApp.ViewModels
{
    public class JokesViewModel : BaseViewModel
    {
        public ICommand GetJokeCommand { get; set; }

        private string _chuckNorrisJoke = "click the button to get a Chuck Norris joke.";

        public string ChuckNorrisJoke
        {
            get { return _chuckNorrisJoke; }
            set { _chuckNorrisJoke = value;
                OnPropertyChanged();
            }
        }
        private string _dadJoke =
            "click the button to get a Dad Joke.";

        public string DadJoke
        {
            get { return _dadJoke; }
            set
            {
                _dadJoke = value; OnPropertyChanged();

            }
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotLoading));
            }
        }

        public bool IsNotLoading
        {
            get { return !IsLoading; }
        }
        private bool _canGetJokes = true;

        
        public bool CanGetJokes
        {
            get { return _canGetJokes; }
            set { _canGetJokes = value; OnPropertyChanged(); }
        }
        private string _errorMessage = ""; 
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(); }
        }
        private IChuckNorrisApiService _chuckNorrisApiService;

        private IDadJokeApiService _dadJokeApiService;
        private IPopUps _popupService;

        public JokesViewModel(IChuckNorrisApiService service, IPopUps popups, IDadJokeApiService dadJokeService)
        {
            _chuckNorrisApiService = service;
            _popupService = popups;
            _dadJokeApiService = dadJokeService;

            GetJokeCommand = new Command(FetchJoke);
        }

        private async void FetchJoke(object param)
        {
            if(IsLoading)
            {
                return;
            }
            IsLoading = true;
            CanGetJokes = false;
            ErrorMessage = "";

            await Task.Delay(2000);


            try
            {
                try
                {

                    //  throw new HttpRequestException("BOOM");
                    ChuckNorrisJoke = await _chuckNorrisApiService.GetJokeAsync();
                }
                catch (Exception ex)
                {
                    await _popupService.ShowPopupAsync("Error", ex.Message, "OK");
                }
                try
                {
                    DadJoke = await _dadJokeApiService.GetDadJokeAsync();
                }
                catch (Exception ex)
                {
                    await _popupService.ShowPopupAsync("Error", ex.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
                CanGetJokes = true;
            }


        }
    }
}
