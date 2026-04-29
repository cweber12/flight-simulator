using FlightX.Input;
using UnityEngine;

namespace FlightX.Aircraft
{
    [RequireComponent(typeof(Rigidbody))]
    public class AircraftController : MonoBehaviour
    {
        [SerializeField, Tooltip("Input adapter used to detect reset requests.")]
        private AircraftInput aircraftInput;

        [SerializeField, Tooltip("Aircraft physics component controlled by crash and reset state.")]
        private AircraftPhysics aircraftPhysics;

        [SerializeField, Tooltip("Fallback crash threshold if no AircraftSettings asset is assigned.")]
        private float fallbackCrashImpactSpeed = 12f;

        private Rigidbody rb;
        private Vector3 startPosition;
        private Quaternion startRotation;

        public bool IsCrashed { get; private set; }
        public float LastImpactSpeed { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            ValidateReferences();
        }

        private void Start()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        private void Reset()
        {
            aircraftInput = GetComponent<AircraftInput>();
            aircraftPhysics = GetComponent<AircraftPhysics>();
        }

        private void OnValidate()
        {
            ValidateReferences();
        }

        private void Update()
        {
            if (aircraftInput != null && aircraftInput.ResetPressed)
            {
                ResetAircraft();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            LastImpactSpeed = collision.relativeVelocity.magnitude;
            if (!IsCrashed && LastImpactSpeed > CrashImpactSpeed)
            {
                EnterCrashedState();
            }
        }

        public void ResetAircraft()
        {
            IsCrashed = false;
            LastImpactSpeed = 0f;

            transform.SetPositionAndRotation(startPosition, startRotation);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (aircraftPhysics != null)
            {
                aircraftPhysics.ResetPhysicsState();
            }
        }

        private void EnterCrashedState()
        {
            IsCrashed = true;

            if (aircraftPhysics != null)
            {
                aircraftPhysics.ResetThrottle();
                aircraftPhysics.SetControlsEnabled(false);
            }
        }

        private float CrashImpactSpeed
        {
            get
            {
                AircraftSettings settings = aircraftPhysics != null ? aircraftPhysics.Settings : null;
                return settings != null ? settings.CrashImpactSpeed : fallbackCrashImpactSpeed;
            }
        }

        private void ValidateReferences()
        {
            if (aircraftInput == null)
            {
                aircraftInput = GetComponent<AircraftInput>();
            }

            if (aircraftPhysics == null)
            {
                aircraftPhysics = GetComponent<AircraftPhysics>();
            }
        }
    }
}
