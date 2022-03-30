using System;
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
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/BombRed.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Blue:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/BombBlue.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Green:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/BombGreen.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Yellow:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/BombYellow.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Pink:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/BombPink.png");
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