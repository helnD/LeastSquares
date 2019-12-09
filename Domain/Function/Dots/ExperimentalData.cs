namespace Domain
{
    public class ExperimentalData : IDot
    {
        public ExperimentalData(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
    }
}