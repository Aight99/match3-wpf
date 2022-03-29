using System.Windows;

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
        private readonly Window _window;
        private GameGrid _gameGrid;
        private GameState _state;
        private Vector2 _selected = Vector2.NullObject;

        public Game(Window window, int gridSize)
        {
            _window = window;
            _gameGrid = new GameGrid(gridSize);
            _state = GameState.FirstClick;
        }

        public Figure GetFigure(Vector2 position) => _gameGrid.GetFigure(position);

        // public string TestMethod(Vector2 position, FigureType type) =>
        //     _gameGrid.GetMatchListAndSpawnBonuses(position, type);

        public void SelectFigure(Vector2 position)
        {
            if (_state == GameState.FirstClick)
            {
                _selected = position;
                _state = GameState.SecondClick;
            }
            else if (_state == GameState.SecondClick)
            {
                if (_selected.IsNearby(position))
                {
                    if (_gameGrid.TryMatch(_selected, position))
                    {
                        // Something
                    }
                }
                _selected = Vector2.NullObject;
                _state = GameState.FirstClick;
            }
        }

        private void ChangeState()
        {
            
        }
    }
}