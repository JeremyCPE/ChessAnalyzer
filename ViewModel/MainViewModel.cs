using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessAnalyzer.Models;
using ChessAnalyzer.Services;
using Microsoft.Maui.Controls;

namespace ChessAnalyzer.ViewModel
{
    public class MainViewModel : BindableObject
    {
        private readonly IChessService _chessService;
        private string _username;
        private bool _isLoading;
        
        public ObservableCollection<Game> Games { get; } = new ();
        public ICommand GetGamesCommand { get; }
        public ICommand LaunchTest { get; }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IChessService chessService)
        {
            _chessService = chessService;
            GetGamesCommand = new Command(async () => await FetchGamesAsync());
            LaunchTest = new Command(async () => await FetchGamesAsyncTest());
        }

        private async Task<object> FetchGamesAsyncTest()
        {
            try
            {
                var games = await _chessService.GetGamesAsyncTest(Username);
                foreach (var game in games)
                {
                    Games.Add(game);
                }
                return Task.FromResult<object>(games);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task FetchGamesAsync()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez entrer un nom d'utilisateur.", "OK");
                return;
            }

            IsLoading = true;
            Games.Clear();

            try
            {
                var games = await _chessService.GetGamesAsync(Username);
                foreach (var game in games)
                {
                    Games.Add(game);
                }
            }
            catch (HttpRequestException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", $"The request to chess.com has failed, please try again later {ex.StatusCode}", "OK");
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Impossible de récupérer les parties. {ex}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}