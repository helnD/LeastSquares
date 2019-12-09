using System.Collections.Generic;
using System.Linq;

namespace Domain.Function.FunctionView.RelativeView
{
    public class RelativeViewBuilder : IViewBuilder
    {
        private readonly Function _function;
        private double _numberOfParts = 50;
        private double _start = 0;
        private double _end = 100;

        public RelativeViewBuilder(Function function)
        {
            _function = function;
        }

        public RelativeViewBuilder NumberOfParts(double number)
        {
            _numberOfParts = number;
            return this;
        }

        public RelativeViewBuilder Start(double start)
        {
            _start = start;
            return this;
        }

        public RelativeViewBuilder End(double end)
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
            
            return new RelativeView(simpleDots);
        }
    }
}