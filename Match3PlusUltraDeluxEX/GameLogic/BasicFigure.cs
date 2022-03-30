using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public class BasicFigure : IFigure
    {
        public FigureType Type { get; set; }
        public Vector2 Position { get; set; }

        public BasicFigure(FigureType type, Vector2 position)
        {
            Type = type;
            Position = position;
        }

        public void Destroy()
        {
            // Score++;
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
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/Red.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Blue:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/Blue.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Green:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/Green.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Yellow:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/Yellow.png");
                    bitmap.EndInit();
                    image.ImageSource = bitmap;
                    break;
                case FigureType.Pink:
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"pack://application:,,,/img/Pink.png");
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

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}