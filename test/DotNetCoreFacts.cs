using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace test
{
    public class DotNetCoreFacts
    {
        [Fact]
        public void MathTruncate_Pass()
        {
            Assert.Equal(8, Math.Truncate(8.96));
        }

        [Fact]
        public void MathRound_Pass()
        {
            Assert.Equal(9, Math.Round(8.96));
        }

        [Fact]
        public void YieldReturn_Pass()
        {
            var list = new List<string>();
            for (int i = 0; i < 5; i++) list.Add($"{i}");
            foreach (var item in list) Debug.WriteLine($"{item}");

            var list2 = MethodA(list);
            foreach (var item in list2) Debug.WriteLine($"{item}");

            Assert.Equal("s0", list2.ToArray()[0]);

        }

        private IEnumerable<string> MethodA(IEnumerable<string> list)
        {
            foreach(var item in list)
            {
                yield return $"s{item}";
                yield return item;
            }
        }
    }
}
