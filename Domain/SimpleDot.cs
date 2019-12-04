namespace Domain
{
    public class SimpleDot : IDot
    {
        public SimpleDot(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }
    }
}