using UnityEngine;

namespace FlightX.Camera
{
    public class ChaseCamera : MonoBehaviour
    {
        [SerializeField, Tooltip("Aircraft or object for the camera to follow.")]
        private Transform target;

        [SerializeField, Tooltip("Follow offset in the target's local space.")]
        private Vector3 offset = new Vector3(0f, 5f, -12f);

        [SerializeField, Tooltip("Seconds used by SmoothDamp to follow the target position.")]
        private float followSmoothTime = 0.18f;

        [SerializeField, Tooltip("Rotation interpolation speed toward the look target.")]
        private float rotationSmoothSpeed = 8f;

        [SerializeField, Tooltip("Distance ahead of the target to look at.")]
        private float lookAheadDistance = 8f;

        private Vector3 followVelocity;

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 desiredPosition = target.TransformPoint(offset);
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref followVelocity, followSmoothTime);

            Vector3 lookTarget = target.position + target.forward * lookAheadDistance;
            Vector3 lookDirection = lookTarget - transform.position;
            if (lookDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection.normalized, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            followVelocity = Vector3.zero;
        }
    }
}
