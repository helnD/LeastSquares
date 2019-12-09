using Domain.Function.FunctionView.AnalyticalView;
using Domain.Function.FunctionView.RelativeView;

namespace WebApplication.Wrappers
{
    public class FunctionViewWrapper
    {
        public FunctionViewWrapper(RelativeView relativeView, AnalyticalView analyticalView)
        {
            RelativeView = relativeView;
            AnalyticalView = analyticalView;
        }

        public RelativeView RelativeView { get; }
        public AnalyticalView AnalyticalView { get; }
    }
}