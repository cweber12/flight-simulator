using UnityEngine;

namespace FlightX.Environment
{
    public class RunwayZone : MonoBehaviour
    {
        [SerializeField, Tooltip("Tag used to identify the player aircraft entering the runway trigger.")]
        private string aircraftTag = "Player";

        public bool IsAircraftInRunwayZone { get; private set; }
        public Transform CurrentAircraft { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsAircraft(other))
            {
                return;
            }

            IsAircraftInRunwayZone = true;
            CurrentAircraft = other.transform;
        }

        private void OnTriggerExit(Collider other)
        {
            if (CurrentAircraft != null && other.transform != CurrentAircraft)
            {
                return;
            }

            if (IsAircraft(other))
            {
                IsAircraftInRunwayZone = false;
                CurrentAircraft = null;
            }
        }

        private bool IsAircraft(Collider other)
        {
            return string.IsNullOrWhiteSpace(aircraftTag) || other.CompareTag(aircraftTag);
        }
    }
}
