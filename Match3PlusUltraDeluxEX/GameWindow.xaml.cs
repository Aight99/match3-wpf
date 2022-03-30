using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public partial class GameWindow : Window
    {
        private const int GridSize = 8;
        private const int CellSizePx = 70;
        private Dictionary<Vector2, Button> _buttons;
        private Game _game;

        public GameWindow()
        {
            InitializeComponent();
            _game = new Game(this, GridSize);
            _buttons = new Dictionary<Vector2, Button>();
            CreateGridLayout();
            _game.Initialize();
            SetContent();
        }

        private void GridClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = (Vector2)button.DataContext;
            _game.SelectFigure(id);
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
                    
                    //
                    // var image = new Image();
                    // image.Source = new BitmapImage(new Uri(@"pack://application:,,,/img/Yellow.png"));
                    // Grid.SetColumn(image, i);
                    // Grid.SetRow(image, j);
                    // image.IsHitTestVisible = false;
                    // GridLayout.Children.Add(image);
                    //
                }
            }
        }

        public void MarkSelected(Vector2 buttonIndex)
        {
            _buttons[buttonIndex].Background = new SolidColorBrush(Colors.Goldenrod);
        }
        
        public void MarkDeselected(Vector2 buttonIndex)
        {
            Color color = ((buttonIndex.X + buttonIndex.Y) % 2 == 0) ? Colors.LightGray : Colors.LightGoldenrodYellow;
            _buttons[buttonIndex].Background = new SolidColorBrush(color);
        }

        // public void SetContent() // Not final visualization
        // {
        //     foreach (var (key, value) in _buttons)
        //     {
        //         if (_game.GetFigure(key).IsNullObject())
        //         {
        //             value.Background = new SolidColorBrush(Colors.Crimson);
        //         }
        //         else
        //         {
        //             value.Background = _game.GetFigure(key).GetImageBrush();
        //         }
        //     }
        // }
        // public void SetContent() // Not final visualization
        // {
        //     foreach (var (key, value) in _buttons)
        //     {
        //         value.Content = _game.GetFigure(key).Position.ToString() + '\n' +
        //                         ((Vector2) value.DataContext).ToString();
        //         if (_game.GetFigure(key).IsNullObject())
        //         {
        //             value.Background = new SolidColorBrush(Colors.Crimson);
        //         }
        //         else
        //         {
        //             value.Background = _game.GetFigure(key).GetImageBrush();
        //         }
        //     }
        // }
        public void SetContent() // Not final visualization
        {
            foreach (var (key, value) in _buttons)
            {
                var ifig = _game.GetFigure(key);

                if (ifig is Bomb)
                {
                    value.Content = "Bomb";
                }
                else if (ifig is VerticalLine)
                {
                    value.Content = "Vert";
                }
                else if (ifig is HorizontalLine)
                {
                    value.Content = "Hori";
                }
                else if (ifig is BasicFigure)
                {
                    value.Content = "Это база";
                }

                if (_game.GetFigure(key).IsNullObject())
                {
                    value.Background = new SolidColorBrush(Colors.Crimson);
                }
                else
                {
                    value.Background = _game.GetFigure(key).GetImageBrush();
                }
            }
        }
    }
}