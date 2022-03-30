using System.Windows.Media;

namespace Match3PlusUltraDeluxEX
{
    public interface IFigure
    {
        public FigureType Type { get; set; }
        public Vector2 Position { get; set; }
        void Destroy();
        ImageBrush GetImageBrush();
        bool IsNullObject();
        void SetNullObject();
    }
}