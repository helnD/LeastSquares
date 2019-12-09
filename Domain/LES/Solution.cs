using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Domain.LES
{
    public class Solution
    {
        private readonly List<double> _result;

        public Solution(List<double> result)
        {
            _result = result;
        }

        public double this[int index] => _result[index];

        public List<double> ToList() =>
            _result.Select(it => it).ToList();
    }
}