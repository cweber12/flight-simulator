using UnityEngine;

namespace FlightX.Aircraft
{
    public static class AircraftGroundHandlingMath
    {
        public static float ComputeSteerAngle(
            float yawInput,
            float forwardSpeedMetersPerSecond,
            float maxSteerAngle,
            float maxSteerSpeedMetersPerSecond)
        {
            float clampedYaw = Mathf.Clamp(yawInput, -1f, 1f);
            float clampedMaxSteerAngle = Mathf.Max(0f, maxSteerAngle);

            if (maxSteerSpeedMetersPerSecond <= 0f)
            {
                return clampedYaw * clampedMaxSteerAngle;
            }

            float speedFactor = Mathf.Clamp01(1f - (Mathf.Abs(forwardSpeedMetersPerSecond) / maxSteerSpeedMetersPerSecond));
            return clampedYaw * clampedMaxSteerAngle * speedFactor;
        }

        public static float ComputeBrakeTorque(bool brakeHeld, float maxBrakeTorque)
        {
            if (!brakeHeld)
            {
                return 0f;
            }

            return Mathf.Max(0f, maxBrakeTorque);
        }
    }
}
