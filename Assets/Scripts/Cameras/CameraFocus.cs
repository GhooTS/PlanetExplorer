using UnityEngine;
using UnityEngine.Events;

namespace GTCamera2D
{
    public class CameraFocus : MonoBehaviour
    {
        public Transform target;
        public UnityEvent onDestinationReach;
        [Range(0, 1)]
        public float smoothTime = 0.3f;
        [Range(0, 1)]
        public float stopDistance = 0.2f;
        private Vector3 velocity;


        private void LateUpdate()
        {
            if (target == null)
                return;
            Vector3 targetPosition = target.position;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            if (Vector3.Distance(transform.position, targetPosition) < .02f)
            {
                onDestinationReach?.Invoke();
                transform.position = targetPosition;
                target = null;
            }
        }

        public void FocusCamera(Transform target)
        {
            this.target = target;
        }

    }
}