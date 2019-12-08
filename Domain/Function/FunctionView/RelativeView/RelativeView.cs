using System.Collections.Generic;

namespace Domain.Function.FunctionView.RelativeView
{
    public class RelativeView : View
    {
        public RelativeView(List<SimpleDot> dots)
        {
            Dots = dots;
        }

        public List<SimpleDot> Dots { get; } 
    }
}