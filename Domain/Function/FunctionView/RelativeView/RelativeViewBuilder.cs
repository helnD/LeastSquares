using System.Collections.Generic;

namespace Domain.Function.FunctionView.RelativeView
{
    public class RelativeViewBuilder : IViewBuilder
    {
        private readonly Function _function;
        private readonly ExperimentalDotsList _experimental;
        private float _numberOfParts = 50;
        private float _start = 0;
        private float _end = 100;

        public RelativeViewBuilder(Function function, List<ExperimentalData> experimental)
        {
            _function = function;
            _experimental = new ExperimentalDotsList(experimental);
        }

        public RelativeViewBuilder NumberOfParts(float number)
        {
            _numberOfParts = number;
            return this;
        }

        public RelativeViewBuilder Start(float start)
        {
            _start = start;
            return this;
        }

        public RelativeViewBuilder End(float end)
        {
            _end = end;
            return this;
        }

        public View Build()
        {
            var step = (_end - _start) / _numberOfParts;
            var simpleDots = new List<SimpleDot>();

            for (var x = _start; x < _end; x += step)
            {
                var y = _function.Value(x);
                simpleDots.Add(new SimpleDot(x, y));
            }
            
            return new RelativeView(new DotsList(simpleDots), _experimental);
        }
    }
}