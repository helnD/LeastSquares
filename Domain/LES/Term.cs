using System;

namespace Domain.LES
{
    public class Term : ICloneable
    {
        public Term(float coefficient, int number)
        {
            Number = number;
            Coefficient = coefficient;
        }

        public float Coefficient { get; }
        public int Number { get; }
        
        public object Clone()
        {
            return new Term(Coefficient, Number);
        }
    }
}