using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Function
{
    public class Function
    {
        private readonly Func<double, double> _function;

        public Function(List<double> parameters)
        {
            Parameters = new Parameters(parameters);
            
            _function = x => parameters.Select((p, i) => p * Math.Pow(x, i)).Sum(); 
        }
        
        public Parameters Parameters { get; }

        public double Value(double argument) =>
            _function(argument);
    }
}