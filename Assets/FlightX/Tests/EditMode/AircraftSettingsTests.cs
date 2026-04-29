using FlightX.Aircraft;
using NUnit.Framework;
using UnityEngine;

namespace FlightX.Tests
{
    public class AircraftSettingsTests
    {
        [Test]
        public void CreateInstanceHasPositivePrototypeDefaults()
        {
            AircraftSettings settings = ScriptableObject.CreateInstance<AircraftSettings>();

            try
            {
                Assert.That(settings.MaxThrust, Is.GreaterThan(0f));
                Assert.That(settings.LiftCoefficient, Is.GreaterThan(0f));
                Assert.That(settings.DragCoefficient, Is.GreaterThan(0f));
                Assert.That(settings.PitchPower, Is.GreaterThan(0f));
                Assert.That(settings.RollPower, Is.GreaterThan(0f));
                Assert.That(settings.YawPower, Is.GreaterThan(0f));
                Assert.That(settings.ThrottleChangeSpeed, Is.GreaterThan(0f));
                Assert.That(settings.StallSpeed, Is.GreaterThan(0f));
                Assert.That(settings.CrashImpactSpeed, Is.GreaterThan(0f));
                Assert.That(settings.MaxSafeLandingVerticalSpeed, Is.GreaterThan(0f));
            }
            finally
            {
                Object.DestroyImmediate(settings);
            }
        }
    }
}
