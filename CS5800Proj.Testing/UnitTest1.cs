using System;
using Xunit;

namespace CS5800Proj.Testing
{
    public class UnitTest1
    {
        [Fact]
        public void OneEqualsOne()
        {
            Assert.True(1==1);
        }

        [Fact]
        public void OneIsNotTwo()
        {
            Assert.False(1 == 2);
        }
    }
}
