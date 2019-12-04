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

        public View Build()
        {
            var viewBuilder = new StringBuilder();

            for (var i = 0; i < _function.Parameters.Count; i++)
            {
                if (Math.Abs(_function.Parameters[i]) < 0.0001) continue;

                var coefficient = Math.Abs(_function.Parameters[i] - 1) > 0.0001 ? _function.Parameters[i].ToString(CultureInfo.InvariantCulture) : "";
                
                switch (i)
                {
                    case 0:
                        viewBuilder.Append(coefficient);
                        break;
                    case 1:
                        viewBuilder.Append(coefficient + "x");
                        break;
                    default:
                        viewBuilder.Append(coefficient + "x^" + i);
                        break;
                }
            }

            return new AnalyticalView(viewBuilder.ToString());
        }
    }
}