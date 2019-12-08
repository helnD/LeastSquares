using System.Collections.Generic;

namespace Domain.Function.FunctionView.RelativeView
{
    public class ExperimentalDotsList
    {
        private readonly List<ExperimentalData> _dots;

        public ExperimentalDotsList(List<ExperimentalData> dots)
        {
            _dots = dots;
        }

        public ExperimentalData this[int index] =>
            _dots[index];
    }
}