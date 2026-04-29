using FlightX.Input;
using UnityEngine;

namespace FlightX.Aircraft
{
    [RequireComponent(typeof(Rigidbody))]
    public class AircraftPhysics : MonoBehaviour
    {
        private const float MetersPerSecondToKnots = 1.94384f;
        private const float MetersToFeet = 3.28084f;
        private const float MetersPerSecondToFeetPerMinute = 196.8504f;

        [SerializeField, Tooltip("Tuning asset for simplified aircraft forces and limits.")]
        private AircraftSettings settings;

        [SerializeField, Tooltip("Input adapter that supplies normalized aircraft controls.")]
        private AircraftInput aircraftInput;

        private Rigidbody rb;
        private float currentThrottle;

        public float CurrentThrottle => currentThrottle;
        public float ForwardSpeedMetersPerSecond { get; private set; }
        public float ForwardSpeedKnots => ForwardSpeedMetersPerSecond * MetersPerSecondToKnots;
        public float AltitudeMeters { get; private set; }
        public float AltitudeFeet => AltitudeMeters * MetersToFeet;
        public float VerticalSpeedMetersPerSecond { get; private set; }
        public float VerticalSpeedFeetPerMinute => VerticalSpeedMetersPerSecond * MetersPerSecondToFeetPerMinute;
        public bool IsStalling { get; private set; }
        public bool IsGrounded { get; private set; }
        public bool ControlsEnabled { get; private set; } = true;
        public AircraftSettings Settings => settings;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            ApplyRigidbodySettings();
            UpdateDerivedState();
        }

        private void Reset()
        {
            aircraftInput = GetComponent<AircraftInput>();
        }

        private void OnValidate()
        {
            if (aircraftInput == null)
            {
                aircraftInput = GetComponent<AircraftInput>();
            }
        }

        private void FixedUpdate()
        {
            if (settings == null)
            {
                UpdateDerivedState();
                return;
            }

            ApplyRigidbodySettings();
            UpdateThrottle();
            ApplyThrust();
            ApplyLift();
            ApplyDrag();
            ApplyControlTorque();
            UpdateDerivedState();
        }

        public void SetControlsEnabled(bool enabled)
        {
            ControlsEnabled = enabled;
        }

        public void ResetPhysicsState()
        {
            currentThrottle = 0f;
            SetControlsEnabled(true);
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            UpdateDerivedState();
        }

        public void ResetThrottle()
        {
            currentThrottle = 0f;
        }

        private void ApplyRigidbodySettings()
        {
            if (rb != null && settings != null)
            {
                rb.angularDrag = settings.AngularDrag;
            }
        }

        private void UpdateThrottle()
        {
            if (!ControlsEnabled)
            {
                return;
            }

            float throttleInput = aircraftInput != null ? aircraftInput.ThrottleInput : 0f;
            currentThrottle = Mathf.Clamp01(currentThrottle + throttleInput * settings.ThrottleChangeSpeed * Time.fixedDeltaTime);
        }

        private void ApplyThrust()
        {
            rb.AddForce(transform.forward * (currentThrottle * settings.MaxThrust), ForceMode.Force);
        }

        private void ApplyLift()
        {
            float forwardSpeed = Mathf.Max(0f, Vector3.Dot(rb.velocity, transform.forward));
            float liftScale = forwardSpeed < settings.StallSpeed ? 0.25f : 1f;
            float liftForce = forwardSpeed * forwardSpeed * settings.LiftCoefficient * liftScale;

            rb.AddForce(transform.up * liftForce, ForceMode.Force);
        }

        private void ApplyDrag()
        {
            Vector3 velocity = rb.velocity;
            float speed = velocity.magnitude;
            if (speed > 0.01f)
            {
                rb.AddForce(-velocity.normalized * (speed * speed * settings.DragCoefficient), ForceMode.Force);
            }

            bool brakeHeld = ControlsEnabled && aircraftInput != null && aircraftInput.BrakeHeld;
            if (brakeHeld)
            {
                rb.AddForce(-velocity * settings.BrakeDrag * rb.mass, ForceMode.Force);
            }
        }

        private void ApplyControlTorque()
        {
            if (!ControlsEnabled || aircraftInput == null)
            {
                return;
            }

            Vector3 localTorque = new Vector3(
                aircraftInput.Pitch * settings.PitchPower,
                aircraftInput.Yaw * settings.YawPower,
                -aircraftInput.Roll * settings.RollPower);

            rb.AddRelativeTorque(localTorque, ForceMode.Force);
        }

        private void UpdateDerivedState()
        {
            if (rb == null)
            {
                return;
            }

            IsGrounded = CheckGrounded();
            ForwardSpeedMetersPerSecond = Vector3.Dot(rb.velocity, transform.forward);
            AltitudeMeters = transform.position.y;
            VerticalSpeedMetersPerSecond = rb.velocity.y;
            IsStalling = !IsGrounded && ForwardSpeedMetersPerSecond < (settings != null ? settings.StallSpeed : 28f);
        }

        private bool CheckGrounded()
        {
            float rayLength = settings != null ? settings.GroundRayLength : 1.6f;
            int layerMask = settings != null ? settings.GroundLayerMask.value : Physics.DefaultRaycastLayers;
            return Physics.Raycast(transform.position, Vector3.down, rayLength, layerMask, QueryTriggerInteraction.Ignore);
        }
    }
}
