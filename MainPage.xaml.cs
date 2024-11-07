namespace ChessAnalyzer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            var pseudo = PseudoEntry.Text;

            if (string.IsNullOrWhiteSpace(pseudo))
            {
                DisplayAlert("Erreur", "Le pseudo ne peut pas être vide", "OK");
            }
            else
            {
                DisplayAlert("Succès", $"Bienvenue, {pseudo}!", "OK");
            }
        }
    }
}