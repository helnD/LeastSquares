using System;

namespace Domain.LES
{
    public class Term : ICloneable
    {
        public Term(double coefficient, int number)
        {
            Number = number;
            Coefficient = coefficient;
        }

        public double Coefficient { get; }
        public int Number { get; }
        
        public object Clone()
        {
            return new Term(Coefficient, Number);
        }
    }
}