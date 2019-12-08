using Microsoft.AspNetCore.Http;

namespace WebApplication.Adapters
{
    public class NumberOfParametersAdapter
    {
        public int Adapt(IQueryCollection query)
        {
            return int.Parse(query["parameters"]);
        }
    }
}