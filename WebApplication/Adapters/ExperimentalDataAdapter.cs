using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Adapters
{
    public class ExperimentalDataAdapter
    {
        public List<ExperimentalData> Adapt(IQueryCollection query)
        {
            var count = int.Parse(query["count"]);
            
            var data = new List<ExperimentalData>();

            for (var i = 0; i < count; i++)
            {
                var x = float.Parse(query["x" + i]);
                var y = float.Parse(query["y" + i]);
                data.Add(new ExperimentalData(x, y));
            }

            return data;
        }
    }
}