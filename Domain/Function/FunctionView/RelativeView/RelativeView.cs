namespace Domain.Function.FunctionView.RelativeView
{
    public class RelativeView : View
    {
        public RelativeView(DotsList dots, ExperimentalDotsList experimentalDots)
        {
            Dots = dots;
            ExperimentalDots = experimentalDots;
        }

        public DotsList Dots { get; }
        public ExperimentalDotsList ExperimentalDots { get; }
    }
}