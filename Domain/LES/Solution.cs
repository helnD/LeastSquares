using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Domain.LES
{
    public class Solution
    {
        private readonly List<float> _result;

        public Solution(List<float> result)
        {
            _result = result;
        }

        public float this[int index] => _result[index];

        public List<float> ToList() =>
            _result.Select(it => it).ToList();
    }
}