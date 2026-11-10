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

        private string _chuckNorrisJoke;

        public string ChuckNorrisJoke
        {
            get { return _chuckNorrisJoke; }
            set { _chuckNorrisJoke = value;
                OnPropertyChanged();
            }
        }

        private IChuckNorrisApiService _chuckNorrisApiService;
        private IPopUps _popupService;

        public JokesViewModel(IChuckNorrisApiService service, IPopUps popups)
        {
            _chuckNorrisApiService = service;
            _popupService = popups;

            GetJokeCommand = new Command(FetchJoke);
        }

        private async void FetchJoke(object param)
        {
            try
            {

              //  throw new HttpRequestException("BOOM");
                ChuckNorrisJoke = await _chuckNorrisApiService.GetJokeAsync();
            }
            catch(Exception ex)
            {
                await _popupService.ShowPopupAsync("Error", ex.Message, "OK");
            }
        }
    }
}
