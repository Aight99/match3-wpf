using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Match3PlusUltraDeluxEX
{
    public partial class GameWindow : Window
    {
        public const int GridSize = 8;
        public const int CellSizePx = 70;
        public const int CanvasTop = 80; // XAML 
        public const int CanvasLeft = 260;
        private const int TimeForGame = 60;

        private readonly Dictionary<Vector2, Image> _images;
        private readonly Dictionary<Vector2, Button> _buttons;
        private readonly Game _game;
        private readonly GameAnimator _animator;
        private DispatcherTimer _timer;
        private bool _isWindowInitialized = false;
        private int _timeSeconds;

        public GameWindow()
        {
            InitializeComponent();
            _game = new Game(this, GridSize);
            _animator = new GameAnimator();
            _buttons = new Dictionary<Vector2, Button>();
            _images = new Dictionary<Vector2, Image>();
            CreateGridLayout();
            _game.Initialize();
            SetVisuals();
            InitializeCounters();
        }

        private void InitializeCounters()
        {
            Game.NullifyScore();
            UpdateScore();
            _timeSeconds = 0;
            _timer = new DispatcherTimer();
            _timer.Tick += UpdateTime;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }
        
        private void UpdateTime(object sender, EventArgs e)
        {
            _timeSeconds++;
            if (_timeSeconds >= TimeForGame)
            {
                _timer.Tick -= UpdateTime;
                var results = new ResultsWindow(_game.GetScore())
                {
                    Top = Top,
                    Left = Left
                };
                results.Show();
                Close();
            }
            TimeText.Text = _timeSeconds.ToString();
        }

        public void MarkSelected(Vector2 buttonIndex)
        {
            _buttons[buttonIndex].Background = new SolidColorBrush(Colors.LightCoral);
        }

        public void MarkDeselected(Vector2 buttonIndex)
        {
            Color color = ((buttonIndex.X + buttonIndex.Y) % 2 == 0) ? Colors.LightGray : Colors.LightGoldenrodYellow;
            _buttons[buttonIndex].Background = new SolidColorBrush(color);
        }

        public void DestroyAnimation()
        { 
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var position = new Vector2(i, j);
                    if (_game.GetFigure(position).IsNullObject)
                    {
                        _animator.DestroyAnimation(_images[position]);
                    }
                }
            }
        }

        public void SwapAnimation(Vector2 firstPosition, Vector2 secondPosition)
        {
            var firstFigure = _images[firstPosition];
            var secondFigure = _images[secondPosition];
            _animator.MoveAnimation(firstFigure, firstPosition, secondPosition);
            _animator.MoveAnimation(secondFigure, secondPosition, firstPosition);
        }
        
        public void PushDownAnimation(List<Vector2> dropsFrom, List<Vector2> dropsTo)
        {
            for (int i = 0; i < dropsFrom.Count; i++)
            {
                var figure = _images[dropsFrom[i]];
                _animator.MoveAnimation(figure, dropsFrom[i], dropsTo[i]);
            }
        }

        public void SetVisuals()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var position = new Vector2(i, j);
                    if (Game.IsInitialized && _isWindowInitialized)
                        CanvasLayout.Children.Remove(_images[position]);

                    var figure = _game.GetFigure(position);
                    if (figure.IsNullObject)
                        continue;
                    
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
            UpdateScore();
            _isWindowInitialized = true;
        }

        private void UpdateScore()
        {
            var score = _game.GetScore();
            ScoreText.Text = score.ToString();
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