using FlightX.Core;
using NUnit.Framework;

namespace FlightX.Tests
{
    public class LandingEvaluatorScoringTests
    {
        [Test]
        public void SmoothOnRunwayTouchdownReturnsSuccessWithHighScore()
        {
            LandingResult result = LandingEvaluator.EvaluateTouchdown(-2f, 45f, true, 4f, 1f);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Score, Is.GreaterThanOrEqualTo(85));
            Assert.That(result.Message, Is.EqualTo("Smooth landing"));
            Assert.That(result.WasOnRunway, Is.True);
        }

        [Test]
        public void OffRunwayTouchdownLowersScoreAndReportsOffRunway()
        {
            LandingResult result = LandingEvaluator.EvaluateTouchdown(-2f, 45f, false, 4f, 1f);

            Assert.That(result.Score, Is.LessThan(100));
            Assert.That(result.Message, Is.EqualTo("Off-runway landing"));
            Assert.That(result.WasOnRunway, Is.False);
        }

        [Test]
        public void ExcessivePitchAndRollLowerScore()
        {
            LandingResult stableResult = LandingEvaluator.EvaluateTouchdown(-2f, 45f, true, 4f, 1f);
            LandingResult unstableResult = LandingEvaluator.EvaluateTouchdown(-2f, 45f, true, 20f, 18f);

            Assert.That(unstableResult.Score, Is.LessThan(stableResult.Score));
            Assert.That(unstableResult.Message, Is.EqualTo("Unstable touchdown"));
        }

        [Test]
        public void HardLandingLowersScore()
        {
            LandingResult smoothResult = LandingEvaluator.EvaluateTouchdown(-2f, 45f, true, 4f, 1f);
            LandingResult hardResult = LandingEvaluator.EvaluateTouchdown(-9f, 45f, true, 4f, 1f);

            Assert.That(hardResult.Score, Is.LessThan(smoothResult.Score));
            Assert.That(hardResult.Message, Is.EqualTo("Hard landing"));
        }

        [Test]
        public void ScoreIsClampedBetweenZeroAndOneHundred()
        {
            LandingResult perfectResult = LandingEvaluator.EvaluateTouchdown(0f, 45f, true, 0f, 0f);
            LandingResult poorResult = LandingEvaluator.EvaluateTouchdown(-200f, 45f, false, 90f, 90f);

            Assert.That(perfectResult.Score, Is.InRange(0, 100));
            Assert.That(poorResult.Score, Is.InRange(0, 100));
            Assert.That(poorResult.Score, Is.EqualTo(0));
        }
    }
}
