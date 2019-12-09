namespace Domain
{
    public class SimpleDot : IDot
    {
        public SimpleDot(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
    }
}