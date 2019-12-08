using System.Collections.Generic;

namespace Domain.Function
{
    public class Parameters
    {
        private readonly List<float> _parameters;

        public Parameters(List<float> parameters)
        {
            _parameters = parameters;
            Count = parameters.Count;
        }
        
        public int Count { get; }

        public float this[int index] =>
            _parameters[index];
    }
}