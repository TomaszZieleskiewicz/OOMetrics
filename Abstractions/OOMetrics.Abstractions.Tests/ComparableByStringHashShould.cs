using OOMetrics.Abstractions.Abstract;

namespace OOMetrics.Abstractions.Tests
{
    public class ComparableByStringHashImplementation : ComparableByStringHash
    {
        public string? value;
        public override string ToString()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return value;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
    public class ComparableByStringHashShould
    {
        [Theory]
        [InlineData("A", "A", true)]
        [InlineData("A", "B", false)]
        [InlineData("A", "", false)]
        [InlineData("A", null, false)]
        [InlineData(null, "B", false)]
        [InlineData(null, null, true)]
        [InlineData("", "", true)]
        [InlineData("A a s df fglkmvr P:sd@#%65", "A a s df fglkmvr P:sd@#%65", true)]
        public void CorrectlyCompareTwoObjects(string value1, string value2, bool result)
        {
            var x = new ComparableByStringHashImplementation() { value = value1 };
            var y = new ComparableByStringHashImplementation() { value = value2 };
            x.Equals(y).Should().Be(result);
        }
        [Theory]
        [InlineData("A")]
        [InlineData(null)]
        public void CorrectlyCompareToNull(string value)
        {
            var x = new ComparableByStringHashImplementation() { value = value };
            ComparableByStringHashImplementation? y = null;
            x.Equals(y).Should().Be(false);
        }
    }
}
