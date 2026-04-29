using System;

namespace FlightX.Core
{
    [Serializable]
    public struct LandingResult
    {
        public bool Success { get; }
        public int Score { get; }
        public string Message { get; }
        public float TouchdownVerticalSpeed { get; }
        public float TouchdownForwardSpeed { get; }
        public bool WasOnRunway { get; }
        public float PitchAngle { get; }
        public float RollAngle { get; }

        public LandingResult(
            bool success,
            int score,
            string message,
            float touchdownVerticalSpeed,
            float touchdownForwardSpeed,
            bool wasOnRunway,
            float pitchAngle,
            float rollAngle)
        {
            Success = success;
            Score = score;
            Message = message;
            TouchdownVerticalSpeed = touchdownVerticalSpeed;
            TouchdownForwardSpeed = touchdownForwardSpeed;
            WasOnRunway = wasOnRunway;
            PitchAngle = pitchAngle;
            RollAngle = rollAngle;
        }
    }
}
