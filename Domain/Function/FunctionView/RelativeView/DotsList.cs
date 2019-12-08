using System.Collections.Generic;

namespace Domain.Function.FunctionView.RelativeView
{
    public class DotsList
    {
        private readonly List<SimpleDot> _dots;

        public DotsList(List<SimpleDot> dots)
        {
            _dots = dots;
        }

        public SimpleDot this[int index] =>
            _dots[index];
    }
}