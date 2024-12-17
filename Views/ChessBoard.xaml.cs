using ChessAnalyzer.Models;
using ChessAnalyzer.ViewModel;

namespace ChessAnalyzer.Views
{
    public partial class ChessBoard : ContentPage
    {
        public ChessBoard(ChessboardViewModel chessboard)
        {
            InitializeComponent();
            BindingContext = chessboard;
        }
    }
}