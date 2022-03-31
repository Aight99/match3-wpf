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
        private readonly int CanvasTop = 80; // XAML 
        private readonly int CanvasLeft = 260;

        private static Dictionary<Vector2, Image> _images;
        private Dictionary<Vector2, Button> _buttons;
        private Game _game;
        private GameAnimator _animator;

        public GameWindow()
        {
            InitializeComponent();
            _game = new Game(this, GridSize);
            _animator = new GameAnimator();
            _buttons = new Dictionary<Vector2, Button>();
            _images = new Dictionary<Vector2, Image>();
            CreateGridLayout();
            SetContent();
            _game.Initialize();
        }

        public void DestroyAnimation(Vector2 position) => _animator.DestroyAnimation(_images[position]);

        public void MarkSelected(Vector2 buttonIndex)
        {
            _buttons[buttonIndex].Background = new SolidColorBrush(Colors.LightCoral);
        }
        
        public void MarkDeselected(Vector2 buttonIndex)
        {
            Color color = ((buttonIndex.X + buttonIndex.Y) % 2 == 0) ? Colors.LightGray : Colors.LightGoldenrodYellow;
            _buttons[buttonIndex].Background = new SolidColorBrush(color);
        }
        public void SetContent()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var position = new Vector2(i, j);
                    if (Game.IsInitialized)
                        CanvasLayout.Children.Remove(_images[position]);
                    var image = new Image
                    {
                        Source = _game.GetFigure(position).GetBitmapImage(),
                        Width = CellSizePx
                    };
                    Canvas.SetTop(image, CanvasTop + CellSizePx * j);
                    Canvas.SetLeft(image, CanvasLeft + CellSizePx * i);
                    image.IsHitTestVisible = false;
                    CanvasLayout.Children.Add(image);
                    _images[position] = image;
                }
            }
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
                }
            }
        }
    }
}