using System;
using System.Collections.Generic;

namespace Match3PlusUltraDeluxEX
{
    public class GameGrid
    {
        // Точка [0,0] - верхний правый угол игрового поля
        private Figure[,] _figures;
        private readonly int _gridSize;

        public GameGrid(int gridSize)
        {
            _gridSize = gridSize;
            _figures = new Figure[_gridSize, _gridSize];
            RandomFill();
        }

        public Figure GetFigure(Vector2 position)
        {
            return _figures[position.X, position.Y];
        }
        
        public bool TryMatch(Vector2 first, Vector2 second)
        {
            var firstType = _figures[first.X, first.Y].Type;
            var secondType = _figures[second.X, second.Y].Type;
            if (firstType == secondType)
            {
                return false;
            }
            
            
            //
            return true;
        }
        
        // Как нужно реализовать этот метод:
        // Мы возвращаем лист фигурок, участвующих в метче, кроме проверяемой (передвинутой)
        // Решение о её судьбе (уничтожение или превращение в бонус) решается на основе размера списка
        // С бомбой всё просто: если размер >= 4, то бомба 
        // С линией сложне: если размер = 3, то ставниваем координаты любого из списка
        // Если X совпадает с передвинутым, то линия вертикальная, иначе - горизонтальная
        // private List<Figure> 
        public string GetMatchListAndSpawnBonuses(Vector2 position, FigureType type)
        {
            int up = 0, down = 0, left = 0, right = 0; // Зачем
            int horCounter = position.X + 1;
            int vertCounter = position.Y + 1;
            var verticalLine = new List<Figure>();
            var horizontalLine = new List<Figure>();
            while (horCounter < _gridSize) // Перекинуть циклы в отдельный метод, чтобы зачем?
            {
                if (_figures[horCounter, position.Y].Type != type)
                    break;
                horizontalLine.Add(_figures[horCounter, position.Y]);
                right++;
                horCounter++;
            }
            while (vertCounter < _gridSize) // Really???
            {
                if (_figures[position.X, vertCounter].Type != type)
                    break;
                verticalLine.Add(_figures[position.X, vertCounter]);
                up++;
                vertCounter++;
            }
            
            horCounter = position.X - 1;
            vertCounter = position.Y - 1;
            while (horCounter >= 0) // WTF 
            {
                if (_figures[horCounter, position.Y].Type != type)
                    break;
                horizontalLine.Add(_figures[horCounter, position.Y]);
                left++;
                horCounter--;
            }
            while (vertCounter >= 0)
            {
                if (_figures[position.X, vertCounter].Type != type)
                    break;
                verticalLine.Add(_figures[position.X, vertCounter]);
                down++;
                vertCounter--;
            }

            // int verticalLineCount = up + down + 1;
            // int horizontalLineCount = right + left + 1;

            if (verticalLine.Count < 2)
            {
                verticalLine.Clear();
            }
            if (horizontalLine.Count < 2)
            {
                horizontalLine.Clear();
            }

            bool isEnoughForBomb = verticalLine.Count + horizontalLine.Count >= 4;
            bool isEnoughForLine = verticalLine.Count + horizontalLine.Count == 3;
            if (isEnoughForBomb)
            {
                return $"Бомба! v:{verticalLine.Count}; h:{horizontalLine.Count}";
            }
            if (isEnoughForLine)
            {
                if (verticalLine.Count == 0 && horizontalLine.Count == 0)
                {
                    return "Упс, что-то пошло не так";
                }
                if (verticalLine.Count != 0 && horizontalLine.Count != 0)
                {
                    return "Упс, что-то пошло не так";
                }
                if (verticalLine.Count != 0)
                {
                    return $"Вертикальная! v:{verticalLine.Count}; h:{horizontalLine.Count}";
                }
                if (horizontalLine.Count != 0)
                {
                    return $"Горизонтальная! v:{verticalLine.Count}; h:{horizontalLine.Count}";
                }
            }
            return $"v:{verticalLine.Count}; h:{horizontalLine.Count}";
        }

        private void RandomFill()
        {
            var random = new Random();
            var figureTypes = Enum.GetValues(typeof(FigureType));
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    _figures[i, j] ??= new Figure((FigureType) figureTypes.GetValue(random.Next(figureTypes.Length)));
                }
            }
        }
    }
}