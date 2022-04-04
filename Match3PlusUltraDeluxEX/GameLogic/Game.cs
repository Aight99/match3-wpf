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
        private readonly GameGrid _gameGrid;
        private GameState _state;
        private Vector2 _selected = Vector2.NullObject;

        private const int SwapDelayMilliseconds = 200;
        private const int VisualsDelayMilliseconds = 100;
        private const int DestroyDelayMilliseconds = 500;
        private const int PushDownDelayMilliseconds = 200;

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
                    await Task.Delay(SwapDelayMilliseconds);
                    _window.SetVisuals();
                    await Task.Delay(VisualsDelayMilliseconds);
                    if (_gameGrid.TryMatch(_selected, position))
                    {
                        _window.MarkDeselected(_selected);
                        _window.DestroyAnimation();
                        await Task.Delay(DestroyDelayMilliseconds);
                        _gameGrid.PushFiguresDown(out var fromList, out var toList); 
                        _window.PushDownAnimation(fromList, toList);
                        await Task.Delay(PushDownDelayMilliseconds);
                        _window.SetVisuals();
                        await Task.Delay(VisualsDelayMilliseconds);
                        _gameGrid.RandomFill();
                        _window.SetVisuals();

                        while (_gameGrid.TryMatchAll())
                        {
                            await Task.Delay(VisualsDelayMilliseconds);
                            _window.DestroyAnimation();
                            await Task.Delay(DestroyDelayMilliseconds);
                            _gameGrid.PushFiguresDown(out fromList, out toList); 
                            _window.PushDownAnimation(fromList, toList);
                            await Task.Delay(PushDownDelayMilliseconds);
                            _window.SetVisuals();
                            await Task.Delay(VisualsDelayMilliseconds);
                            _gameGrid.RandomFill();
                            _window.SetVisuals();
                        }
                        
                    }
                    else
                    {
                        _window.MarkDeselected(_selected);
                        SwapFigures(position);
                        await Task.Delay(SwapDelayMilliseconds);
                        _window.SetVisuals();
                        await Task.Delay(VisualsDelayMilliseconds);
                    }
                }
                else
                {
                    _window.MarkDeselected(_selected);
                }
                _selected = Vector2.NullObject;
                _state = GameState.FirstClick;
            }
        }

        private void SwapFigures(Vector2 position)
        {
            _gameGrid.SwapFigures(_selected, position);
            _window.SwapAnimation(_selected, position);
        }

        public void Initialize()
        {
            while (_gameGrid.TryMatchAll()) {};
            IsInitialized = true;
        }

        public int GetScore() => _score;
        public static void AddScore(int points) => _score += points;
        public static void NullifyScore() => _score = 0;
    }
}