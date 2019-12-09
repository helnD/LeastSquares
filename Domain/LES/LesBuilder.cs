using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.LES
{
    public class LesBuilder
    {
        
        private int _numberOfParameters = 4;
        private List<ExperimentalData> _data;

        public LesBuilder(List<ExperimentalData> data)
        {
            Data(data);
        }

        public LesBuilder NumberOfParameters(int number)
        {
            _numberOfParameters = number;
            return this;
        }
        
        public LesBuilder Data(List<ExperimentalData> data)
        {
            _data = data;
            return this;
        }

        public LinearEquationSystem Build()
        {
            var powerNumber = 2 * _numberOfParameters - 1;
            var powers = new List<double>
            {
                _numberOfParameters
            };

            for (var i = 1; i < powerNumber; i++)
            {
                powers.Add(_data.Sum(it => Math.Pow(it.X, i)));
            }

            var equations = new List<LinearEquation>();
            for (var i = 0; i < _numberOfParameters; i++)
            {
                var result = _data.Sum(it => Math.Pow(it.X, i) * it.Y);
                var terms = new List<double>();
                
                for (var j = i; j < i + _numberOfParameters; j++)
                {
                    terms.Add(powers[j]);
                }
                
                equations.Add(new LinearEquation(result, terms.ToArray()));
            }
            
            return new LinearEquationSystem(equations);
        }
    }
}