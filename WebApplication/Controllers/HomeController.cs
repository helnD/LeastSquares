using System.Linq;
using Domain.Function;
using Domain.Function.FunctionView.AnalyticalView;
using Domain.Function.FunctionView.RelativeView;
using Domain.LES;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Adapters;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Calculate()
        {
            var number = new NumberOfParametersAdapter().Adapt(Request.Query);
            var data = new ExperimentalDataAdapter().Adapt(Request.Query);
            
            var system = new LesBuilder(data)
                .NumberOfParameters(number)
                .Build();

            var function = new Function(system.Solution().ToList());

            var relativeView = new RelativeViewBuilder(function)
                .Start(data.Min(it => it.X))
                .End(190)
                .NumberOfParts(1000)
                .Build();

            var analyticView = new AnalyticalViewBuilder(function).Build();

            var response = new { relativeView, analyticView };

            return Json(response);
        }
    }
}