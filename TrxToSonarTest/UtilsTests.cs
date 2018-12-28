using TrxToSonar;
using Xunit;

namespace TrxToSonarTest
{
    public class UtilsTests
    {
        [Fact]
        public void TrxDurationShouldBeConverted()
        {
            var sonarDuration = Utils.ToSonarDuration("00:00:00.0090000");
            Assert.Equal(9, sonarDuration);
        }
    }
}