using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Match3PlusUltraDeluxEX
{
    public partial class MainWindow : Window
    {
        private const int GridSize = 8;
        private const int CellSizePx = 70;
        private Dictionary<Vector2, Button> _buttons;

        private Game _game;

        public MainWindow()
        {
            InitializeComponent();
            _game = new Game(this, GridSize);
            _buttons = new Dictionary<Vector2, Button>();
            CreateGridLayout();
            
            SetContent();
        }

        private void GridClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = (Vector2)button.DataContext;
            // MessageBox.Show(id.ToString());
            // button.Content = _game.GetFigure(id);
        }

        private void CreateGridLayout()
        {
            for (int i = 0; i < GridSize; i++)
            {
                var column = new ColumnDefinition
                {
                    Width = new GridLength(CellSizePx)
                };
                var row = new RowDefinition()
                {
                    Height = new GridLength(CellSizePx)
                };
                GridLayout.ColumnDefinitions.Add(column);
                GridLayout.RowDefinitions.Add(row);
            }

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    Color color = ((i + j) % 2 == 0) ? Colors.LightGray : Colors.LightGoldenrodYellow;
                    var button = new Button()
                    {
                        Background = new SolidColorBrush(color),
                        BorderThickness = new Thickness(0),
                        DataContext = new Vector2(i, j)
                    };
                    button.Click += GridClick;
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                    GridLayout.Children.Add(button);

                    _buttons[new Vector2(i, j)] = button;
                }
            }
        }

        private void SetContent() // Just for now
        {
            foreach (var (key, value) in _buttons)
            {
                value.Content = _game.GetFigure(key);
            }
        }
    }
}