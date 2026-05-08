using FlightX.Input;
using UnityEngine;

namespace FlightX.Aircraft
{
    [RequireComponent(typeof(Rigidbody))]
    public class AircraftLandingGear : MonoBehaviour
    {
        [System.Serializable]
        private struct WheelVisualBinding
        {
            [Tooltip("Physical wheel collider.")]
            public WheelCollider wheelCollider;

            [Tooltip("Visual wheel mesh transform to sync from the collider pose.")]
            public Transform visualTransform;
        }

        [SerializeField, Tooltip("Input adapter used for nose wheel steering and braking.")]
        private AircraftInput aircraftInput;

        [Header("Wheel Colliders")]
        [SerializeField, Tooltip("Nose wheel used for steering and braking.")]
        private WheelCollider noseWheel;

        [SerializeField, Tooltip("Left main wheel used for braking.")]
        private WheelCollider leftMainWheel;

        [SerializeField, Tooltip("Right main wheel used for braking.")]
        private WheelCollider rightMainWheel;

        [Header("Ground Handling")]
        [SerializeField, Tooltip("Maximum nose wheel steering angle in degrees at low speed.")]
        private float maxNoseSteerAngle = 22f;

        [SerializeField, Tooltip("Steering authority fades out toward this forward speed in m/s.")]
        private float maxSteerSpeedMetersPerSecond = 30f;

        [SerializeField, Tooltip("Brake torque applied to all wheels when the brake input is held.")]
        private float maxBrakeTorque = 4000f;

        [Header("Visuals")]
        [SerializeField, Tooltip("Optional visual wheel transforms to sync with wheel colliders.")]
        private WheelVisualBinding[] wheelVisuals;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            ValidateReferences();
        }

        private void Reset()
        {
            aircraftInput = GetComponent<AircraftInput>();
        }

        private void OnValidate()
        {
            ValidateReferences();
        }

        private void FixedUpdate()
        {
            float forwardSpeedMetersPerSecond = rb != null
                ? Vector3.Dot(rb.linearVelocity, transform.forward)
                : 0f;

            float yawInput = aircraftInput != null ? aircraftInput.Yaw : 0f;
            bool brakeHeld = aircraftInput != null && aircraftInput.BrakeHeld;

            float steerAngle = AircraftGroundHandlingMath.ComputeSteerAngle(
                yawInput,
                forwardSpeedMetersPerSecond,
                maxNoseSteerAngle,
                maxSteerSpeedMetersPerSecond);

            float brakeTorque = AircraftGroundHandlingMath.ComputeBrakeTorque(brakeHeld, maxBrakeTorque);

            if (noseWheel != null)
            {
                noseWheel.steerAngle = steerAngle;
            }

            ApplyBrakeTorque(leftMainWheel, brakeTorque);
            ApplyBrakeTorque(rightMainWheel, brakeTorque);
            ApplyBrakeTorque(noseWheel, brakeTorque);
        }

        private void Update()
        {
            if (wheelVisuals == null)
            {
                return;
            }

            for (int i = 0; i < wheelVisuals.Length; i++)
            {
                WheelCollider wheelCollider = wheelVisuals[i].wheelCollider;
                Transform visualTransform = wheelVisuals[i].visualTransform;
                if (wheelCollider == null || visualTransform == null)
                {
                    continue;
                }

                wheelCollider.GetWorldPose(out Vector3 worldPosition, out Quaternion worldRotation);
                visualTransform.SetPositionAndRotation(worldPosition, worldRotation);
            }
        }

        private void ValidateReferences()
        {
            if (aircraftInput == null)
            {
                aircraftInput = GetComponent<AircraftInput>();
            }
        }

        private static void ApplyBrakeTorque(WheelCollider wheel, float brakeTorque)
        {
            if (wheel != null)
            {
                wheel.brakeTorque = brakeTorque;
            }
        }
    }
}
