using UnityEngine;
using UnityEngine.InputSystem;

namespace FlightX.Input
{
    public class AircraftInput : MonoBehaviour
    {
        [Header("Axis Actions")]
        [SerializeField, Tooltip("Pitch axis action. Positive input pitches the nose up.")]
        private InputActionReference pitchAction;

        [SerializeField, Tooltip("Roll axis action. Positive input rolls right.")]
        private InputActionReference rollAction;

        [SerializeField, Tooltip("Yaw axis action. Positive input yaws right.")]
        private InputActionReference yawAction;

        [SerializeField, Tooltip("Throttle axis action. Positive increases throttle, negative decreases it.")]
        private InputActionReference throttleAction;

        [Header("Button Actions")]
        [SerializeField, Tooltip("Brake button action.")]
        private InputActionReference brakeAction;

        [SerializeField, Tooltip("Reset aircraft button action.")]
        private InputActionReference resetAction;

        [SerializeField, Tooltip("Camera switch button action.")]
        private InputActionReference cameraSwitchAction;

        [SerializeField, Tooltip("Pause button action.")]
        private InputActionReference pauseAction;

        public float Pitch => ReadFloat(pitchAction);
        public float Roll => ReadFloat(rollAction);
        public float Yaw => ReadFloat(yawAction);
        public float ThrottleInput => ReadFloat(throttleAction);
        public bool BrakeHeld => IsPressed(brakeAction);
        public bool ResetPressed => WasPressedThisFrame(resetAction);
        public bool CameraSwitchPressed => WasPressedThisFrame(cameraSwitchAction);
        public bool PausePressed => WasPressedThisFrame(pauseAction);

        private void OnEnable()
        {
            EnableAction(pitchAction);
            EnableAction(rollAction);
            EnableAction(yawAction);
            EnableAction(throttleAction);
            EnableAction(brakeAction);
            EnableAction(resetAction);
            EnableAction(cameraSwitchAction);
            EnableAction(pauseAction);
        }

        private void OnDisable()
        {
            DisableAction(pitchAction);
            DisableAction(rollAction);
            DisableAction(yawAction);
            DisableAction(throttleAction);
            DisableAction(brakeAction);
            DisableAction(resetAction);
            DisableAction(cameraSwitchAction);
            DisableAction(pauseAction);
        }

        private static float ReadFloat(InputActionReference actionReference)
        {
            InputAction action = actionReference != null ? actionReference.action : null;
            return action != null ? action.ReadValue<float>() : 0f;
        }

        private static bool IsPressed(InputActionReference actionReference)
        {
            InputAction action = actionReference != null ? actionReference.action : null;
            return action != null && action.IsPressed();
        }

        private static bool WasPressedThisFrame(InputActionReference actionReference)
        {
            InputAction action = actionReference != null ? actionReference.action : null;
            return action != null && action.WasPressedThisFrame();
        }

        private static void EnableAction(InputActionReference actionReference)
        {
            InputAction action = actionReference != null ? actionReference.action : null;
            if (action != null && !action.enabled)
            {
                action.Enable();
            }
        }

        private static void DisableAction(InputActionReference actionReference)
        {
            InputAction action = actionReference != null ? actionReference.action : null;
            if (action != null && action.enabled)
            {
                action.Disable();
            }
        }
    }
}
