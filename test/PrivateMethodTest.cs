using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace test
{
    public class PrivateMethodTest
    {
        [Fact]
        public void MathTruncate_Pass()
        {
            Assert.Equal(8, Math.Truncate(8.96));
        }
    }
}
