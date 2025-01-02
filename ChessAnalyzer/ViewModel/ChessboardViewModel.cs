using ChessAnalyzer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ChessAnalyzer.ViewModel;
public class ChessboardViewModel : BindableObject
{
    private int _currentMoveIndex;
    private List<string> _pgnMoves; // Holds extracted moves
    private string[,] _board;

    public ObservableCollection<ChessSquare> Squares { get; set; } = new();
    public string CurrentMoveLabel => $"Move: {_currentMoveIndex + 1}/{_pgnMoves.Count}";

    public ICommand NextMoveCommand { get; }
    public ICommand PreviousMoveCommand { get; }

    public Game SelectedGame { get; private set; }

    public ChessboardViewModel()
    {
        NextMoveCommand = new Command(ExecuteNextMove);
        PreviousMoveCommand = new Command(ExecutePreviousMove);

        LoadInitialBoard();
        ParsePGN(SamplePGN); // Pass the PGN
    }

    // PGN to parse
    private const string SamplePGN = @"
[Event ""Live Chess""]
[Site ""Chess.com""]
[Date ""2024.11.11""]
[Round ""-""]
[White ""RedSeeds""]
[Black ""mustafa2266""]
[Result ""0-1""]
[CurrentPosition ""r1b5/ppR4R/4pkp1/4n3/4p1r1/8/PPP3P1/2K5 w - -""]

1. e4 {[%clk 0:10:00]} 1... g6 {[%clk 0:09:57.4]} 
2. d4 {[%clk 0:09:55.6]} 2... Bg7 {[%clk 0:09:56.3]} 
3. Nf3 {[%clk 0:09:54.9]} 3... e6 {[%clk 0:09:55.7]} 4. h4 {[%clk 0:09:48.8]} ";


    public void SetGame(Game game)
    {
        SelectedGame = game;
        OnPropertyChanged(nameof(SelectedGame));
    }

    // PropertyChanged implementation (simplified example)
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void LoadInitialBoard()
    {
        _board = new string[8, 8]
        {
            { "br", "bn", "bb", "bq", "bk", "bb", "bn", "br" },
            { "bp", "bp", "bp", "bp", "bp", "bp", "bp", "bp" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "wp", "wp", "wp", "wp", "wp", "wp", "wp", "wp" },
            { "wr", "wn", "wb", "wq", "wk", "wb", "wn", "wr" }
        };
        RefreshBoard();
    }

    private void ParsePGN(string pgn)
    {
        // Regex to extract only the moves (e.g., e4, g6)
        Regex moveRegex = new(@"\b[a-h][1-8](?:=[QRBN])?|[a-hRNBQK][a-h1-8x]*[a-h1-8](?:=[QRBN])?\+?#?");

        _pgnMoves = moveRegex.Matches(pgn)
                             .Select(m => m.Value)
                             .ToList();

        _currentMoveIndex = -1; // Start before the first move
    }

    private void ExecuteNextMove()
    {
        if (_currentMoveIndex + 1 < _pgnMoves.Count)
        {
            _currentMoveIndex++;
            ApplyMove(_pgnMoves[_currentMoveIndex]);
            RefreshBoard();
            OnPropertyChanged(nameof(CurrentMoveLabel));
        }
    }

    private void ExecutePreviousMove()
    {
        if (_currentMoveIndex >= 0)
        {
            _currentMoveIndex--;
            LoadInitialBoard();
            for (int i = 0; i <= _currentMoveIndex; i++)
                ApplyMove(_pgnMoves[i]);

            RefreshBoard();
            OnPropertyChanged(nameof(CurrentMoveLabel));
        }
    }

    private void ApplyMove(string move)
    {
        //TODO : Use ChessModel to apply the move
        int targetFile = move[^2] - 'a';
        int targetRank = 8 - int.Parse(move[^1].ToString());

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (_board[row, col].StartsWith("w") || _board[row, col].StartsWith("b"))
                {
                    _board[targetRank, targetFile] = _board[row, col];
                    _board[row, col] = "";
                    return;
                }
            }
        }
    }

    private void RefreshBoard()
    {
        Squares.Clear();
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                bool isDarkSquare = (row + col) % 2 == 1;
                string imageSource = !string.IsNullOrEmpty(_board[row, col]) ? $"{_board[row, col]}.png" : "";

                Squares.Add(new ChessSquare
                {
                    Row = row.ToString(),
                    Column = col.ToString(),
                    Color = isDarkSquare ? System.Drawing.Color.Brown : System.Drawing.Color.Beige,
                    PieceImage = imageSource
                });
            }
        }
    }
}
