using FlightX.Aircraft;
using FlightX.Environment;
using UnityEngine;

namespace FlightX.Core
{
    public class LandingEvaluator : MonoBehaviour
    {
        [SerializeField, Tooltip("Aircraft physics source used to detect touchdown and measure speeds.")]
        private AircraftPhysics aircraftPhysics;

        [SerializeField, Tooltip("Aircraft controller used to ignore crash touchdowns.")]
        private AircraftController aircraftController;

        [SerializeField, Tooltip("Runway trigger zone used to determine whether touchdown happened on runway.")]
        private RunwayZone runwayZone;

        [SerializeField, Tooltip("Pitch angle beyond this value is considered unstable at touchdown.")]
        private float maxStablePitchAngle = 12f;

        [SerializeField, Tooltip("Roll angle beyond this value is considered unstable at touchdown.")]
        private float maxStableRollAngle = 8f;

        public LandingResult LastLandingResult { get; private set; }
        public bool HasLandingResult { get; private set; }

        private bool wasGrounded;

        private void Start()
        {
            wasGrounded = aircraftPhysics != null && aircraftPhysics.IsGrounded;
        }

        private void Update()
        {
            if (aircraftPhysics == null)
            {
                return;
            }

            bool isGrounded = aircraftPhysics.IsGrounded;
            bool isCrashed = aircraftController != null && aircraftController.IsCrashed;
            if (!wasGrounded && isGrounded && !isCrashed)
            {
                EvaluateTouchdown();
            }

            wasGrounded = isGrounded;
        }

        private void EvaluateTouchdown()
        {
            bool wasOnRunway = runwayZone != null && runwayZone.IsAircraftInRunwayZone;
            float verticalSpeed = aircraftPhysics.VerticalSpeedMetersPerSecond;
            float forwardSpeed = aircraftPhysics.ForwardSpeedMetersPerSecond;
            float pitchAngle = NormalizeAngle(aircraftPhysics.transform.eulerAngles.x);
            float rollAngle = NormalizeAngle(aircraftPhysics.transform.eulerAngles.z);

            int score = 100;
            float safeVerticalSpeed = aircraftPhysics.Settings != null ? aircraftPhysics.Settings.MaxSafeLandingVerticalSpeed : 4f;
            float verticalSpeedPenalty = Mathf.Max(0f, Mathf.Abs(verticalSpeed) - safeVerticalSpeed) * 12f;
            score -= Mathf.RoundToInt(verticalSpeedPenalty);

            if (!wasOnRunway)
            {
                score -= 35;
            }

            score -= Mathf.RoundToInt(Mathf.Max(0f, Mathf.Abs(pitchAngle) - maxStablePitchAngle) * 2f);
            score -= Mathf.RoundToInt(Mathf.Max(0f, Mathf.Abs(rollAngle) - maxStableRollAngle) * 3f);
            score = Mathf.Clamp(score, 0, 100);

            string message = BuildMessage(score, verticalSpeed, safeVerticalSpeed, wasOnRunway, pitchAngle, rollAngle);
            LastLandingResult = new LandingResult(
                score >= 60,
                score,
                message,
                verticalSpeed,
                forwardSpeed,
                wasOnRunway,
                pitchAngle,
                rollAngle);

            HasLandingResult = true;
        }

        private string BuildMessage(float score, float verticalSpeed, float safeVerticalSpeed, bool wasOnRunway, float pitchAngle, float rollAngle)
        {
            if (!wasOnRunway)
            {
                return "Off-runway landing";
            }

            if (Mathf.Abs(verticalSpeed) > safeVerticalSpeed * 1.5f)
            {
                return "Hard landing";
            }

            if (Mathf.Abs(pitchAngle) > maxStablePitchAngle || Mathf.Abs(rollAngle) > maxStableRollAngle)
            {
                return "Unstable touchdown";
            }

            return score >= 85f ? "Smooth landing" : "Stable landing";
        }

        private static float NormalizeAngle(float angle)
        {
            return angle > 180f ? angle - 360f : angle;
        }
    }
}
