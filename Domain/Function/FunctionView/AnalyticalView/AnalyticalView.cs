using System.Collections.Generic;

namespace Domain.Function.FunctionView.AnalyticalView
{
    public class AnalyticalView : View
    {
        public AnalyticalView(List<float> parameters)
        {
            Parameters = parameters;
        }

        public List<float> Parameters { get; }
    }
}