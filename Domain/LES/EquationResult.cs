using System;

namespace Domain.LES
{
    public class EquationResult : ICloneable
    {
        public EquationResult(double value)
        {
            Value = value;
        }

        public double Value { get; }
        public int Number { get; }
        
        public object Clone()
        {
            return new EquationResult(Value);
        }
    }
}