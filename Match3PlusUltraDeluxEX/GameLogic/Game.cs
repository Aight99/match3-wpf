using System.Threading.Tasks;

namespace Match3PlusUltraDeluxEX
{
    public enum GameState
    {
        FirstClick,
        SecondClick,
        Animation
    }
    public class Game
    {
        public static bool IsInitialized { get; private set; } = false;
        private static int _score;
        private readonly GameWindow _window;
        private GameGrid _gameGrid;
        private GameState _state;
        private Vector2 _selected = Vector2.NullObject;

        public Game(GameWindow window, int gridSize)
        {
            _window = window;
            _gameGrid = new GameGrid(gridSize);
            _state = GameState.FirstClick;
        }

        public IFigure GetFigure(Vector2 position) => _gameGrid.GetFigure(position);

        public async void SelectFigure(Vector2 position)
        {
            if (_state == GameState.FirstClick)
            {
                _selected = position;
                _window.MarkSelected(_selected);
                _state = GameState.SecondClick;
            }
            else if (_state == GameState.SecondClick)
            {
                if (_selected.IsNearby(position))
                {
                    _state = GameState.Animation;
                    SwapFigures(position);
                    _window.SetVisuals();
                    await Task.Delay(100);
                    if (_gameGrid.TryMatch(_selected, position))
                    {
                        _window.DestroyAnimation();
                        await Task.Delay(500);
                        _gameGrid.PushFiguresDown();
                        _window.SetVisuals();
                        await Task.Delay(500);
                        _gameGrid.RandomFill();
                        _window.SetVisuals();

                    }
                    else
                    {
                        SwapFigures(position);
                        _window.SetVisuals();
                        await Task.Delay(100);
                    }
                }
                _window.MarkDeselected(_selected);
                _selected = Vector2.NullObject;
                _state = GameState.FirstClick;
            }
        }

        private void SwapFigures(Vector2 position)
        {
            _gameGrid.SwapFigures(_selected, position);
            // Swap animation
        }

        public void Initialize()
        {
            while (_gameGrid.TryMatchAll()) {};
            IsInitialized = true;
        }

        public static void AddScore(int points) => _score += points;
        public static void NullifyScore(int points) => _score = 0;
    }
}