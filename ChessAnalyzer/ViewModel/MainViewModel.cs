using ChessAnalyser.Service;
using ChessAnalyzer.Models;
using ChessAnalyzer.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChessAnalyzer.ViewModel
{
    public class MainViewModel : BindableObject
    {
        private readonly IChessService _chessService;
        private readonly IServiceProvider _serviceProvider;


        private string _username;
        private bool _isLoading;

        public ObservableCollection<Game> Games { get; } = new();

        public ICommand AnalyseGameCommand { get; }
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

        public MainViewModel(IChessService chessService, IServiceProvider serviceProvider)
        {
            _chessService = chessService;
            _serviceProvider = serviceProvider;
            GetGamesCommand = new Command(async () => await FetchGamesAsync());
            LaunchTest = new Command(async () => await FetchGamesAsyncTest());
            AnalyseGameCommand = new Command<Game>(NavigateToChessBoard);

        }

        private async void NavigateToChessBoard(Game selectedGame)
        {
            if (selectedGame == null) return;

            ChessBoard? chessBoardPage = _serviceProvider.GetService<ChessBoard>();

            if (chessBoardPage.BindingContext is ChessboardViewModel viewModel)
            {
                viewModel.SetGame(selectedGame);
            }

            // Navigate to the ChessBoard page
            await AppShell.Current.Navigation.PushAsync(chessBoardPage);
        }

        private async Task<object> FetchGamesAsyncTest()
        {
            try
            {
                List<Game> games = await _chessService.GetGamesAsyncTest(Username);
                foreach (Game game in games)
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
                List<Game> games = await _chessService.GetGamesAsync(Username);
                foreach (Game game in games)
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