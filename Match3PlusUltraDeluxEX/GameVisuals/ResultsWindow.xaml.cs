using System.Windows;

namespace Match3PlusUltraDeluxEX
{
    public partial class ResultsWindow : Window
    {
        public ResultsWindow(int finalScore)
        {
            InitializeComponent();
            FinalScore.Text = "Score: " + finalScore;
        }

        private void BackInMenu(object sender, RoutedEventArgs e)
        {
            var menu = new MenuWindow
            {
                Top = Top,
                Left = Left
            };
            menu.Show();
            Close();
        }
    }
}