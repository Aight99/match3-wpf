using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public class Bomb : IFigure
    {
        public Bomb(IFigure figure)
        {
            Position = figure.Position;
            Type = figure.Type;
        }

        public FigureType Type { get; set; }
        public Vector2 Position { get; set; }
        public bool IsNullObject { get; private set; }

        public void Destroy(IFigure[,] list)
        {
            if (IsNullObject)
                return;
            IsNullObject = true;
            ActivateBonus(list);
        }

        private void ActivateBonus(IFigure[,] list)
        {
            if (Position.Y > 0)
            {
                list[Position.X, Position.Y - 1].Destroy(list);
                if (Position.X > 0)
                {
                    list[Position.X - 1, Position.Y - 1].Destroy(list);
                }
                if (Position.X < GameWindow.GridSize - 1)
                {
                    list[Position.X + 1, Position.Y - 1].Destroy(list);
                }
            }
            if (Position.Y < GameWindow.GridSize - 1)
            {
                list[Position.X, Position.Y + 1].Destroy(list);
                if (Position.X > 0)
                {
                    list[Position.X - 1, Position.Y + 1].Destroy(list);
                }
                if (Position.X < GameWindow.GridSize - 1)
                {
                    list[Position.X + 1, Position.Y + 1].Destroy(list);
                }
            }
            if (Position.X > 0)
            {
                list[Position.X - 1, Position.Y].Destroy(list);
            }
            if (Position.X < GameWindow.GridSize - 1)
            {
                list[Position.X + 1, Position.Y].Destroy(list);
            }
        }

        public BitmapImage GetBitmapImage()
        {
            Uri uriSource;
            switch (Type)
            {
                case FigureType.Red:
                    uriSource = new Uri(@"pack://application:,,,/img/BombRed.png");
                    return new BitmapImage(uriSource);
                case FigureType.Blue:
                    uriSource = new Uri(@"pack://application:,,,/img/BombBlue.png");
                    return new BitmapImage(uriSource);
                case FigureType.Green:
                    uriSource = new Uri(@"pack://application:,,,/img/BombGreen.png");
                    return new BitmapImage(uriSource);
                case FigureType.Yellow:
                    uriSource = new Uri(@"pack://application:,,,/img/BombYellow.png");
                    return new BitmapImage(uriSource);
                case FigureType.Pink:
                    uriSource = new Uri(@"pack://application:,,,/img/BombPink.png");
                    return new BitmapImage(uriSource);
                default:
                    uriSource = new Uri(@"pack://application:,,,/img/Bomb.png");
                    return new BitmapImage(uriSource);
            }
        }
    }
}