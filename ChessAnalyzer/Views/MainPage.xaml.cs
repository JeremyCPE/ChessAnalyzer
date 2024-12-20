using ChessAnalyzer.ViewModel;

namespace ChessAnalyzer.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly Color _defaultColor = Colors.LightBlue;
        private readonly Color _selectedColor = Colors.DarkBlue;
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void PseudoEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(PseudoEntry.Text))
            {
                SubmitButton.BackgroundColor = _defaultColor;
                return;
            }

            SubmitButton.BackgroundColor = _selectedColor;
        }
    }
}