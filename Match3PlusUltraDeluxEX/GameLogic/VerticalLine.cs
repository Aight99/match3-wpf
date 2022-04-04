using System;
using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public class VerticalLine : IFigure
    {
        public VerticalLine(IFigure figure)
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
            for (int i = 0; i < GameWindow.GridSize; i++)
            {
                list[Position.X, i].Destroy(list);
            }
        }

        public BitmapImage GetBitmapImage()
        {
            Uri uriSource;
            switch (Type)
            {
                case FigureType.Red:
                    uriSource = new Uri(@"pack://application:,,,/img/LineRed.png");
                    return new BitmapImage(uriSource);
                case FigureType.Blue:
                    uriSource = new Uri(@"pack://application:,,,/img/LineBlue.png");
                    return new BitmapImage(uriSource);
                case FigureType.Green:
                    uriSource = new Uri(@"pack://application:,,,/img/LineGreen.png");
                    return new BitmapImage(uriSource);
                case FigureType.Yellow:
                    uriSource = new Uri(@"pack://application:,,,/img/LineYellow.png");
                    return new BitmapImage(uriSource);
                case FigureType.Pink:
                    uriSource = new Uri(@"pack://application:,,,/img/LinePink.png");
                    return new BitmapImage(uriSource);
                default:
                    uriSource = new Uri(@"pack://application:,,,/img/Line.png");
                    return new BitmapImage(uriSource);
            }
        }
    }
}