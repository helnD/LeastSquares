using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Function
{
    public class Function
    {
        private readonly Func<float, float> _function;

        public Function(List<float> parameters)
        {
            Parameters = new Parameters(parameters);
            
            _function = x => (float)parameters.Select((p, i) => p * Math.Pow(x, i)).Sum(); 
        }
        
        public Parameters Parameters { get; }

        public float Value(float argument) =>
            _function(argument);
    }
}