using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.LES
{
    public class LinearEquation : ICloneable
    {
        private readonly List<Term> _terms;
        private readonly EquationResult _result;

        public LinearEquation(float result, params float[] terms)
        {
            Count = terms.Length;
            _result = new EquationResult(result);

            int index = 0;
            _terms = terms.Select(it => new Term(it, index++))
                .ToList();
        }
        
        public int Count { get; }

        public float Result => _result.Value;

        public LinearEquation Plus(LinearEquation equation)
        {
            var terms = new float[this.Count];

            for (int index = 0; index < this.Count; index++)
            {
                terms[index] = this[index] + equation[index];
            }
            var result = equation.Result + Result;
            
            return new LinearEquation(result, terms);
        }

        public LinearEquation Multiply(float number)
        {
            var terms = new float[this.Count];

            for (int index = 0; index < this.Count; index++)
            {
                terms[index] = this[index] * number;
            }

            var result = Result * number;
            
            return new LinearEquation(result, terms);
        }
        
        public float this[int index] => _terms[index].Coefficient;
        
        public object Clone()
        {
            return new LinearEquation(Result, _terms.Select(it => it.Coefficient).ToArray());
        }
    }
}