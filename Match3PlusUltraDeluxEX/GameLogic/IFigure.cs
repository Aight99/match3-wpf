using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public interface IFigure
    {
        FigureType Type { get; set; }
        Vector2 Position { get; set; }
        void Destroy();
        BitmapImage GetBitmapImage();
        bool IsNullObject();
        void SetNullObject();
    }
}