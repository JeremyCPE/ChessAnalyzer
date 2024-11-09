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
        
        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();
        public ICommand GetGamesCommand { get; }

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