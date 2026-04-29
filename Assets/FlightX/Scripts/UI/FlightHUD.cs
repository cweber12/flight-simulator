using FlightX.Aircraft;
using TMPro;
using UnityEngine;

namespace FlightX.UI
{
    public class FlightHUD : MonoBehaviour
    {
        [SerializeField, Tooltip("Aircraft physics source for speed, altitude, vertical speed, and throttle.")]
        private AircraftPhysics aircraftPhysics;

        [SerializeField, Tooltip("Aircraft controller source for crash state.")]
        private AircraftController aircraftController;

        [Header("Text")]
        [SerializeField, Tooltip("Displays indicated speed in knots.")]
        private TextMeshProUGUI speedText;

        [SerializeField, Tooltip("Displays altitude in feet.")]
        private TextMeshProUGUI altitudeText;

        [SerializeField, Tooltip("Displays vertical speed in feet per minute.")]
        private TextMeshProUGUI verticalSpeedText;

        [SerializeField, Tooltip("Displays throttle percentage.")]
        private TextMeshProUGUI throttleText;

        [SerializeField, Tooltip("Displays high-level aircraft state.")]
        private TextMeshProUGUI statusText;

        [SerializeField, Tooltip("Displays stall and crash warnings.")]
        private TextMeshProUGUI warningText;

        private void Update()
        {
            if (aircraftPhysics == null)
            {
                ClearText();
                return;
            }

            SetText(speedText, $"Speed: {aircraftPhysics.ForwardSpeedKnots:0} kt");
            SetText(altitudeText, $"Altitude: {aircraftPhysics.AltitudeFeet:0} ft");
            SetText(verticalSpeedText, $"VS: {aircraftPhysics.VerticalSpeedFeetPerMinute:0} ft/min");
            SetText(throttleText, $"Throttle: {aircraftPhysics.CurrentThrottle * 100f:0}%");
            SetText(statusText, $"Status: {GetStatus()}");
            SetText(warningText, GetWarning());
        }

        private string GetStatus()
        {
            if (aircraftController != null && aircraftController.IsCrashed)
            {
                return "Crashed";
            }

            if (aircraftPhysics.IsStalling)
            {
                return "Stalling";
            }

            return aircraftPhysics.IsGrounded ? "Grounded" : "Airborne";
        }

        private string GetWarning()
        {
            if (aircraftController != null && aircraftController.IsCrashed)
            {
                return "CRASH - Press Reset";
            }

            return aircraftPhysics.IsStalling ? "STALL" : string.Empty;
        }

        private void ClearText()
        {
            SetText(speedText, string.Empty);
            SetText(altitudeText, string.Empty);
            SetText(verticalSpeedText, string.Empty);
            SetText(throttleText, string.Empty);
            SetText(statusText, "Status: No Aircraft");
            SetText(warningText, string.Empty);
        }

        private static void SetText(TextMeshProUGUI text, string value)
        {
            if (text != null)
            {
                text.text = value;
            }
        }
    }
}
