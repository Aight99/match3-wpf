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

        public void Destroy()
        {
            SetNullObject();
        }

        public ImageBrush GetImageBrush()
        {
            var image = new ImageBrush();
            var bitmap = new BitmapImage();
            switch (Type)
            {
                case FigureType.Red:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/LineRedH.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Blue:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/LineBlueH.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Green:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/LineGreenH.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Yellow:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/LineYellowH.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Pink:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/LinePinkH.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
            }
            return image;
        }

        public bool IsNullObject()
        {
            return Position.Equals(Vector2.NullObject);
        }

        public void SetNullObject()
        {
            Position = Vector2.NullObject;
        }
    }
}