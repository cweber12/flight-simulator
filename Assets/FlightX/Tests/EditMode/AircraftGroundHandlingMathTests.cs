using FlightX.Aircraft;
using NUnit.Framework;

namespace FlightX.Tests
{
    public class AircraftGroundHandlingMathTests
    {
        [Test]
        public void ComputeSteerAngleIsMaxAtLowSpeed()
        {
            float steer = AircraftGroundHandlingMath.ComputeSteerAngle(
                yawInput: 1f,
                forwardSpeedMetersPerSecond: 0f,
                maxSteerAngle: 20f,
                maxSteerSpeedMetersPerSecond: 30f);

            Assert.That(steer, Is.EqualTo(20f).Within(0.001f));
        }

        [Test]
        public void ComputeSteerAngleFadesToZeroAtHighSpeed()
        {
            float steer = AircraftGroundHandlingMath.ComputeSteerAngle(
                yawInput: 1f,
                forwardSpeedMetersPerSecond: 60f,
                maxSteerAngle: 20f,
                maxSteerSpeedMetersPerSecond: 30f);

            Assert.That(steer, Is.EqualTo(0f).Within(0.001f));
        }

        [Test]
        public void ComputeBrakeTorqueUsesConfiguredTorqueWhenHeld()
        {
            float torque = AircraftGroundHandlingMath.ComputeBrakeTorque(
                brakeHeld: true,
                maxBrakeTorque: 4500f);

            Assert.That(torque, Is.EqualTo(4500f));
        }

        [Test]
        public void ComputeBrakeTorqueIsZeroWhenNotHeld()
        {
            float torque = AircraftGroundHandlingMath.ComputeBrakeTorque(
                brakeHeld: false,
                maxBrakeTorque: 4500f);

            Assert.That(torque, Is.EqualTo(0f));
        }
    }
}
