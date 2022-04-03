using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public class HorizontalLine : IFigure
    {
        public HorizontalLine(IFigure figure)
        {
            Position = figure.Position;
            Type = figure.Type;
        }

        public FigureType Type { get; set; }
        public Vector2 Position { get; set; }
        public bool IsNullObject { get; private set; }

        public void Destroy()
        {
            IsNullObject = true;
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