using System;
using System.Globalization;
using System.Text;

namespace Domain.Function.FunctionView.AnalyticalView
{
    public class AnalyticalViewBuilder : IViewBuilder
    {

        private readonly Function _function;

        public AnalyticalViewBuilder(Function function)
        {
            _function = function;
        }

        public View Build() =>
            new AnalyticalView(_function.Parameters.ToFloatList());
    }
}