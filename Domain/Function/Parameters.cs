using System.Collections.Generic;
using System.Linq;

namespace Domain.Function
{
    public class Parameters
    {
        private readonly List<double> _parameters;

        public Parameters(List<double> parameters)
        {
            _parameters = parameters;
            Count = parameters.Count;
        }
        
        public int Count { get; }

        public double this[int index] =>
            _parameters[index];

        public List<double> ToFloatList() =>
            _parameters.ToList();
    }
}