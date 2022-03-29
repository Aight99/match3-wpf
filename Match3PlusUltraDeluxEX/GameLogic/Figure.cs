namespace Match3PlusUltraDeluxEX
{
    public class Figure
    {
        public FigureType Type { get; }

        public Figure(FigureType type)
        {
            Type = type;
        }
        
        public virtual void Destroy() // Maybe return int score
        {
            // Score++;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}