using UnityEngine;

namespace FlightX.Aircraft
{
    [CreateAssetMenu(menuName = "FlightX/Aircraft Settings", fileName = "DefaultAircraftSettings")]
    public class AircraftSettings : ScriptableObject
    {
        [Header("Forces")]
        [SerializeField, Tooltip("Maximum forward thrust in Newtons at full throttle.")]
        private float maxThrust = 18000f;

        [SerializeField, Tooltip("Simplified lift multiplier applied from forward speed squared.")]
        private float liftCoefficient = 1.8f;

        [SerializeField, Tooltip("Simplified drag multiplier applied opposite the Rigidbody velocity.")]
        private float dragCoefficient = 0.025f;

        [SerializeField, Tooltip("Rigidbody angular drag applied while this aircraft is active.")]
        private float angularDrag = 0.5f;

        [Header("Controls")]
        [SerializeField, Tooltip("Pitch torque strength.")]
        private float pitchPower = 8500f;

        [SerializeField, Tooltip("Roll torque strength.")]
        private float rollPower = 11000f;

        [SerializeField, Tooltip("Yaw torque strength.")]
        private float yawPower = 3500f;

        [SerializeField, Tooltip("Throttle change rate per second.")]
        private float throttleChangeSpeed = 0.35f;

        [Header("Limits")]
        [SerializeField, Tooltip("Forward speed below which lift is reduced heavily, in meters per second.")]
        private float stallSpeed = 28f;

        [SerializeField, Tooltip("Maximum safe touchdown vertical speed in meters per second.")]
        private float maxSafeLandingVerticalSpeed = 4f;

        [SerializeField, Tooltip("Collision relative speed that marks the aircraft as crashed.")]
        private float crashImpactSpeed = 12f;

        [Header("Grounding")]
        [SerializeField, Tooltip("Extra velocity-proportional drag applied while braking.")]
        private float brakeDrag = 3.5f;

        [SerializeField, Tooltip("Downward ray length used to detect whether the aircraft is grounded.")]
        private float groundRayLength = 1.6f;

        [SerializeField, Tooltip("Layers considered ground for simple aircraft grounding checks.")]
        private LayerMask groundLayerMask = ~0;

        public float MaxThrust => maxThrust;
        public float LiftCoefficient => liftCoefficient;
        public float DragCoefficient => dragCoefficient;
        public float AngularDrag => angularDrag;
        public float PitchPower => pitchPower;
        public float RollPower => rollPower;
        public float YawPower => yawPower;
        public float ThrottleChangeSpeed => throttleChangeSpeed;
        public float StallSpeed => stallSpeed;
        public float MaxSafeLandingVerticalSpeed => maxSafeLandingVerticalSpeed;
        public float CrashImpactSpeed => crashImpactSpeed;
        public float BrakeDrag => brakeDrag;
        public float GroundRayLength => groundRayLength;
        public LayerMask GroundLayerMask => groundLayerMask;
    }
}
