using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class DotNetCoreTheories
    {
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(9)]
        public void Odd_Pass(int value)
        {
            Assert.True(IsOdd(value));
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(-5)]
        [InlineData(-7)]
        [InlineData(-9)]
        public void NegetiveOdd_Pass(int value)
        {
            Assert.False(IsOdd(value));
        }

        public bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}
