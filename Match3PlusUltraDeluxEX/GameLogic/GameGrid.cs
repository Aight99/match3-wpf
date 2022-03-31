using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Match3PlusUltraDeluxEX
{
    public class GameGrid
    {
        // Точка [0,0] - верхний правый угол игрового поля
        private IFigure[,] _figures;
        private readonly int _gridSize;

        public GameGrid(int gridSize)
        {
            _gridSize = gridSize;
            _figures = new IFigure[_gridSize, _gridSize];
            RandomFill();
        }

        public IFigure GetFigure(Vector2 position)
        {
            return _figures[position.X, position.Y];
        }

        private void SwapFigures(int x1, int y1, int x2, int y2)
        {
            (_figures[x1, y1].Position, _figures[x2, y2].Position) = (_figures[x2, y2].Position, _figures[x1, y1].Position);
            (_figures[x1, y1], _figures[x2, y2]) = (_figures[x2, y2], _figures[x1, y1]);
        }

        public bool TryMatchAll()
        {
            bool isMatched = false;
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    if (ExecuteMatch(new Vector2(i, j), ref _figures[i, j]))
                    {
                        isMatched = true;
                    }
                }
            }
            RandomFill();
            return isMatched;
        }
        
        public bool TryMatch(Vector2 firstPosition, Vector2 secondPosition) // Вообще стоит вынести смену местами
        {
            var firstFigure = _figures[firstPosition.X, firstPosition.Y];
            var secondFigure = _figures[secondPosition.X, secondPosition.Y];
            if (firstFigure.Type == secondFigure.Type)
            {
                return false;
            }

            SwapFigures(firstPosition.X, firstPosition.Y, secondPosition.X, secondPosition.Y);
            // firstFigure = _figures[firstPosition.X, firstPosition.Y];
            // secondFigure = _figures[secondPosition.X, secondPosition.Y];

            // var firstTry = !ExecuteMatch(secondPosition, secondFigure);
            // var secondTry = !ExecuteMatch(firstPosition, firstFigure);
            var firstTry = !ExecuteMatch(secondPosition, ref _figures[secondPosition.X, secondPosition.Y]);
            var secondTry = !ExecuteMatch(firstPosition, ref _figures[firstPosition.X, firstPosition.Y]);
            
            if (firstTry && secondTry)
            {
                (_figures[firstPosition.X, firstPosition.Y], _figures[secondPosition.X, secondPosition.Y]) = (_figures[secondPosition.X, secondPosition.Y], _figures[firstPosition.X, firstPosition.Y]);
            }
            
            // PushFiguresDown(); // Вынести 
            // while (TryMatchAll()) {};// Вынести 
            return true;
        }

        public void PushFiguresDown()
        {
            // Напоминаю, что [0,0] - верхний правый угол игрового поля
            // Проходим каждый столбец снизу-вверх
            for (int i = 0; i < _gridSize; i++)
            {
                int gap = 0;
                for (int j = _gridSize - 1; j >= 0; j--)
                {
                    if (_figures[i, j].IsNullObject())
                    {
                        gap++;
                    }
                    else
                    {
                        SwapFigures(i, j + gap, i, j);
                    }
                }
            }
        }

        private bool ExecuteMatch(Vector2 position, ref IFigure firstFigure)
        {
            var matchList = GetMatchList(position, firstFigure.Type);
            
            if (matchList.Count == 0)
                return false;
            
            if (Game.IsInitialized && !TrySetBonus(matchList, ref firstFigure))
            {
                matchList.Add(firstFigure);
            }
            foreach (var figure in matchList)
            {
                figure.Destroy();
            }
            return true;
        }

        private bool TrySetBonus(List<IFigure> match, ref IFigure figureToSet)
        {
            bool isEnoughForBomb = match.Count >= 4;
            bool isEnoughForLine = match.Count == 3;
            if (isEnoughForBomb)
            {
                MessageBox.Show("Бомба!");
                figureToSet = (IFigure) new Bomb(figureToSet);
                // figureToSet = new Bomb(figureToSet);
                return true;
            }
            if (isEnoughForLine)
            {
                if (match[0].Position.X == figureToSet.Position.X)
                {
                    MessageBox.Show("Вертикаль!");
                    figureToSet = (IFigure) new VerticalLine(figureToSet);
                    // figureToSet = new VerticalLine(figureToSet);
                }
                else
                {
                    MessageBox.Show("Горизонталь!");
                    figureToSet = (IFigure) new HorizontalLine(figureToSet);
                    // figureToSet = new HorizontalLine(figureToSet);
                }
                return true;
            }

            return false;
        }

        // Мы возвращаем лист фигурок, участвующих в метче, кроме проверяемой (передвинутой)

        // Решение о её судьбе (уничтожение или превращение в бонус) решается на основе размера списка

        // С бомбой всё просто: если размер >= 4, то бомба 

        // С линией сложне: если размер = 3, то ставниваем координаты любого из списка

        // Если X совпадает с передвинутым, то линия вертикальная, иначе - горизонтальная

        private List<IFigure> GetMatchList(Vector2 position, FigureType type)
        {
            int horCounter = position.X + 1;
            int vertCounter = position.Y + 1;
            var verticalLine = new List<IFigure>();
            var horizontalLine = new List<IFigure>();
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
                    if (_figures[i, j] == null  || _figures[i, j].IsNullObject())
                        _figures[i, j] = new BasicFigure(randomType, new Vector2(i, j));
                }
            }
        }
    }
}