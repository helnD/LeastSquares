using System;

namespace Domain.LES
{
    public class EquationResult : ICloneable
    {
        public EquationResult(float value)
        {
            Value = value;
        }

        public float Value { get; }
        public int Number { get; }
        
        public object Clone()
        {
            return new EquationResult(Value);
        }
    }
}