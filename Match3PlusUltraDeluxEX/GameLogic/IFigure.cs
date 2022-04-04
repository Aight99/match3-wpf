using System.Windows.Media.Imaging;

namespace Match3PlusUltraDeluxEX
{
    public interface IFigure
    {
        FigureType Type { get; set; }
        Vector2 Position { get; set; }
        bool IsNullObject { get; }
        void Destroy(IFigure[,] list);
        BitmapImage GetBitmapImage();
    }
}