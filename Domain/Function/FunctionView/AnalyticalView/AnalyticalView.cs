namespace Domain.Function.FunctionView.AnalyticalView
{
    public class AnalyticalView : View
    {
        public AnalyticalView(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}