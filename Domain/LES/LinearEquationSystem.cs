using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.LES
{
    public class LinearEquationSystem : ICloneable
    {
        private readonly List<LinearEquation> _equations;
        private readonly int _count;

        public LinearEquationSystem(List<LinearEquation> equations)
        {
            _equations = equations;
            _count = equations.Count;
        }

        public Solution Solution()
        {
            LinearEquationSystem stepMatrix = StepMatrix(this);
            var solutions = new List<double>();
            
            for (var i = stepMatrix._count - 1; i >= 0; i--)
            {
                var subtrahend = 0.0;
                for (var j = i + 1; j < stepMatrix._count; j++)
                {
                    subtrahend += stepMatrix[i][j] * solutions[^(j - i)];
                }

                var solution = stepMatrix[i].Result - subtrahend;
                
                solutions.Add(solution);
            }

            solutions.Reverse();
            return new Solution(solutions);
        }
        
        public LinearEquationSystem ReplaceRow(int index, LinearEquation equation)
        {
            var result = _equations.Select(it => it.Clone() as LinearEquation).ToList();
            result[index] = equation.Clone() as LinearEquation;
            return new LinearEquationSystem(result);
        }

        public LinearEquation this[int index] =>
            _equations[index];

        public object Clone()
        {
            return new LinearEquationSystem(_equations.Select(it => it.Clone() as LinearEquation).ToList());
        }
        
        private LinearEquationSystem StepMatrix(LinearEquationSystem system)
        {
            var result = system.Clone() as LinearEquationSystem;

            for (var i = 0; ; i++)
            {
                var firstElement = result[i][i];
                var newRow = result[i].Multiply(1.0 / firstElement);

                result = result.ReplaceRow(i, newRow);
                
                if (i + 1 == result._count) break;

                for (var j = i + 1; j < result._count; j++)
                {
                    var multiplier = result[j][i] > 0 ? -1 : 1;

                    newRow = result[i].Multiply(multiplier * Math.Abs(result[j][i]))
                        .Plus(result[j]);
                    result = result.ReplaceRow(j, newRow);
                }
            }

            return result;
        }
    }
}