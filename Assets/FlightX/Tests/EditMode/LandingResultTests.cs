using FlightX.Core;
using NUnit.Framework;

namespace FlightX.Tests
{
    public class LandingResultTests
    {
        [Test]
        public void ConstructorStoresExpectedValues()
        {
            LandingResult result = new LandingResult(
                true,
                92,
                "Smooth landing",
                -2.5f,
                42f,
                true,
                4f,
                -2f);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Score, Is.EqualTo(92));
            Assert.That(result.Message, Is.EqualTo("Smooth landing"));
            Assert.That(result.TouchdownVerticalSpeed, Is.EqualTo(-2.5f));
            Assert.That(result.TouchdownForwardSpeed, Is.EqualTo(42f));
            Assert.That(result.WasOnRunway, Is.True);
            Assert.That(result.PitchAngle, Is.EqualTo(4f));
            Assert.That(result.RollAngle, Is.EqualTo(-2f));
        }
    }
}
