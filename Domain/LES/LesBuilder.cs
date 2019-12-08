using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.LES
{
    public class LesBuilder
    {
        
        private int _numberOfParameters = 4;
        private List<ExperimentalData> _data = new List<ExperimentalData>
        {
            new ExperimentalData(0, 0)
        };

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
            var powers = new List<float>
            {
                _numberOfParameters
            };

            for (var i = 1; i < powerNumber; i++)
            {
                powers.Add((float)_data.Sum(it => Math.Pow(it.X, i)));
            }

            var equations = new List<LinearEquation>();
            for (var i = 0; i < _numberOfParameters; i++)
            {
                var result = (float)_data.Sum(it => Math.Pow(it.X, i) * it.Y);
                var terms = new List<float>();
                
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