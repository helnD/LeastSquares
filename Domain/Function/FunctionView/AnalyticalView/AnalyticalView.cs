using System.Collections.Generic;

namespace Domain.Function.FunctionView.AnalyticalView
{
    public class AnalyticalView : View
    {
        public AnalyticalView(List<double> parameters)
        {
            Parameters = parameters;
        }

        public List<double> Parameters { get; }
    }
}