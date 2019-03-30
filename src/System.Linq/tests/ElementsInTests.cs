using Xunit;

namespace System.Linq.Tests
{
    public class ElementsInTests : EnumerableTests
    {
        [Fact]
        public void ElementsInrangeTest()
        {
            int[] source = Enumerable.Range(0, 10).ToArray();
            Assert.Equal(source, source.ElementsIn(new Range(0, ^0)));
            Assert.Equal(source.Skip(1).Take(4), source.ElementsIn(new Range(1, 5)));
            Assert.Equal(source.Skip(1).Take(4), source.ElementsIn(new Range(1, ^5)));
            Assert.Equal(source.Skip(1).Take(4), source.ElementsIn(new Range(^9, ^5)));
            Assert.Equal(source.Skip(1).Take(4), source.ElementsIn(new Range(^9, 5)));
        }
    }
}
