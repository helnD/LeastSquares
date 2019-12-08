namespace Domain
{
    public class ExperimentalData : IDot
    {
        public ExperimentalData(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }
    }
}