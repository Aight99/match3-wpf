using System.Windows.Media;

namespace Match3PlusUltraDeluxEX
{
    public interface IFigure
    {
        FigureType Type { get; set; }
        Vector2 Position { get; set; }
        void Destroy();
        ImageBrush GetImageBrush();
        bool IsNullObject();
        void SetNullObject();
    }
}