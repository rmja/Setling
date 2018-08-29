using Xunit;

namespace Setling.Tests
{
    public class SettleRuleBuilderTests
    {
        [Fact]
        public void ShouldReturnEmptyRuleByDefault()
        {
            Assert.Equal(string.Empty, new SettleRuleBuilder().Rule.ToString());
        }
    }
}
