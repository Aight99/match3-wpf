using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public class BasicFigure : IFigure
    {
        public FigureType Type { get; set; }
        public Vector2 Position { get; set; }
        
        public bool IsNullObject { get; private set; }

        public BasicFigure(FigureType type, Vector2 position)
        {
            Type = type;
            Position = position;
        }

        public void Destroy()
        {
            // Score++;
            IsNullObject = true;
        }

        public BitmapImage GetBitmapImage()
        {
            Uri uriSource;
            switch (Type)
            {
                case FigureType.Red:
                    uriSource = new Uri(@"pack://application:,,,/img/Red.png");
                    return new BitmapImage(uriSource);
                case FigureType.Blue:
                    uriSource = new Uri(@"pack://application:,,,/img/Blue.png");
                    return new BitmapImage(uriSource);
                case FigureType.Green:
                    uriSource = new Uri(@"pack://application:,,,/img/Green.png");
                    return new BitmapImage(uriSource);
                case FigureType.Yellow:
                    uriSource = new Uri(@"pack://application:,,,/img/Yellow.png");
                    return new BitmapImage(uriSource);
                case FigureType.Pink:
                    uriSource = new Uri(@"pack://application:,,,/img/Pink.png");
                    return new BitmapImage(uriSource);
                default:
                    uriSource = new Uri(@"pack://application:,,,/img/Yellow.png");
                    return new BitmapImage(uriSource);
            }
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}