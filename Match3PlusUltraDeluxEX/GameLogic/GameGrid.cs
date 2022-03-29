using System;
using System.Collections.Generic;
using System.Windows;

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
        
        public bool TryMatch(Vector2 firstPosition, Vector2 secondPosition)
        {
            var firstFigure = _figures[firstPosition.X, firstPosition.Y];
            var secondFigure = _figures[secondPosition.X, secondPosition.Y];
            if (firstFigure.Type == secondFigure.Type)
            {
                return false;
            }
            
            ExecuteMatch(firstPosition, firstFigure);
            ExecuteMatch(secondPosition, secondFigure);
            return true;
        }

        private void ExecuteMatch(Vector2 position, Figure firstFigure)
        {
            var matchList = GetMatchList(position, firstFigure.Type);
            if (!TrySetBonus(matchList, firstFigure))
            {
                matchList.Add(firstFigure);
            }
            foreach (var figure in matchList)
            {
                figure.Destroy();
            }
        }
        
        private bool TrySetBonus(List<Figure> match, Figure figureToSet)
        {
            bool isEnoughForBomb = match.Count >= 4;
            bool isEnoughForLine = match.Count == 3;
            if (isEnoughForBomb)
            {
                MessageBox.Show("Бомба!");
                // figure = bomb
                return true;
            }
            if (isEnoughForLine)
            {
                if (match[0].Position.X == figureToSet.Position.X)
                {
                    MessageBox.Show("Горизонталь!");
                    // figure = horizontalLine
                }
                else
                {
                    MessageBox.Show("Вертикаль!");
                    // figure = verticalLine
                }
                return true;
            }

            return false;
        }

        // Как нужно реализовать этот метод:
        // Мы возвращаем лист фигурок, участвующих в метче, кроме проверяемой (передвинутой)
        // Решение о её судьбе (уничтожение или превращение в бонус) решается на основе размера списка
        // С бомбой всё просто: если размер >= 4, то бомба 
        // С линией сложне: если размер = 3, то ставниваем координаты любого из списка
        // Если X совпадает с передвинутым, то линия вертикальная, иначе - горизонтальная
        private List<Figure> GetMatchList(Vector2 position, FigureType type)
        {
            int horCounter = position.X + 1;
            int vertCounter = position.Y + 1;
            var verticalLine = new List<Figure>();
            var horizontalLine = new List<Figure>();
            while (horCounter < _gridSize) 
            {
                if (_figures[horCounter, position.Y].Type != type)
                    break;
                horizontalLine.Add(_figures[horCounter, position.Y]);
                horCounter++;
            }
            while (vertCounter < _gridSize)
            {
                if (_figures[position.X, vertCounter].Type != type)
                    break;
                verticalLine.Add(_figures[position.X, vertCounter]);
                vertCounter++;
            }
            
            horCounter = position.X - 1;
            vertCounter = position.Y - 1;
            while (horCounter >= 0) 
            {
                if (_figures[horCounter, position.Y].Type != type)
                    break;
                horizontalLine.Add(_figures[horCounter, position.Y]);
                horCounter--;
            }
            while (vertCounter >= 0)
            {
                if (_figures[position.X, vertCounter].Type != type)
                    break;
                verticalLine.Add(_figures[position.X, vertCounter]);
                vertCounter--;
            }
            
            if (verticalLine.Count < 2)
            {
                verticalLine.Clear();
            }
            if (horizontalLine.Count < 2)
            {
                horizontalLine.Clear();
            }
            
            verticalLine.AddRange(horizontalLine);
            return verticalLine;
        }

        private void RandomFill()
        {
            var random = new Random();
            var figureTypes = Enum.GetValues(typeof(FigureType));
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    var randomType = (FigureType)figureTypes.GetValue(random.Next(figureTypes.Length));
                    _figures[i, j] ??= new Figure(randomType, new Vector2(i, j));
                }
            }
        }
    }
}