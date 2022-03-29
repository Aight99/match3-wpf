namespace Match3PlusUltraDeluxEX
{
    public class Figure
    {
        public FigureType Type { get; }
        public Vector2 Position { get; set; }

        public Figure(FigureType type, Vector2 position)
        {
            Type = type;
            Position = position;
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