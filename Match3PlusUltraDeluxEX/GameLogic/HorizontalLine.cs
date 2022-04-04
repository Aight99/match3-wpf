using System;
using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public class HorizontalLine : IFigure
    {
        public FigureType Type { get; set; }
        public Vector2 Position { get; set; }
        public bool IsNullObject { get; private set; }

        private const int PointsForDestroying = 100;
        
        public HorizontalLine(IFigure figure)
        {
            Position = figure.Position;
            Type = figure.Type;
        }

        public void Destroy(IFigure[,] list)
        {
            if (IsNullObject)
                return;
            Game.AddScore(PointsForDestroying);
            IsNullObject = true;
            ActivateBonus(list);
        }
        
        private void ActivateBonus(IFigure[,] list)
        {
            for (int i = 0; i < GameWindow.GridSize; i++)
            {
                list[i, Position.Y].Destroy(list);
            }
        }

        public BitmapImage GetBitmapImage()
        {
            Uri uriSource;
            switch (Type)
            {
                case FigureType.Red:
                    uriSource = new Uri(@"pack://application:,,,/img/LineRedH.png");
                    return new BitmapImage(uriSource);
                case FigureType.Blue:
                    uriSource = new Uri(@"pack://application:,,,/img/LineBlueH.png");
                    return new BitmapImage(uriSource);
                case FigureType.Green:
                    uriSource = new Uri(@"pack://application:,,,/img/LineGreenH.png");
                    return new BitmapImage(uriSource);
                case FigureType.Yellow:
                    uriSource = new Uri(@"pack://application:,,,/img/LineYellowH.png");
                    return new BitmapImage(uriSource);
                case FigureType.Pink:
                    uriSource = new Uri(@"pack://application:,,,/img/LinePinkH.png");
                    return new BitmapImage(uriSource);
                default:
                    uriSource = new Uri(@"pack://application:,,,/img/Line.png");
                    return new BitmapImage(uriSource);
            }
        }
    }
}