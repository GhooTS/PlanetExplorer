using UnityEngine;

namespace GTCamera2D
{
    [RequireComponent(typeof(Camera))]
    public class CameraFreeLook : MonoBehaviour
    {
        [Tooltip("main if not set")]
        new public Camera camera;
        public Vector3 Offset = new Vector3(0, 0, -10);
        [Header("Controls")]
        [SerializeReference]
        private ICameraInput input = new AxisCameraInput();

        [Header("Move options")]
        public float speed = 25.0f;
        public bool limitCameraMovement = false;
        public bool countCameraSize = false;
        public MovementLimiter moveLimitation;





        private void Start()
        {
            if (camera == null)
            {
                camera = GetComponent<Camera>();
            }
        }

        private void LateUpdate()
        {
            //Get current camera position
            Vector3 newPosition = transform.position;

            //Get player inputs
            newPosition = GetPlayerInputs(newPosition);

            //Limt camera movement to bounds
            newPosition = GetNewPosition(newPosition);

            //set new camera position
            transform.position = newPosition + Offset;
        }

        private Vector3 GetNewPosition(Vector3 newPosition)
        {
            if (limitCameraMovement)
            {
                if (countCameraSize)
                {
                    //Get size of orthographic camera
                    Vector2 cameraSize = new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize);
                    //if camera size is greater than camera movement bounds, set camera position to the center of the bounds
                    if (cameraSize.x * 2 > moveLimitation.bounds.size.x + moveLimitation.margin
                        || cameraSize.y * 2 > moveLimitation.bounds.size.y + moveLimitation.margin)
                    {
                        newPosition = moveLimitation.bounds.center;
                    }
                    else
                    {
                        newPosition = moveLimitation.GetPosition(newPosition, cameraSize);
                    }
                }
                else
                {
                    newPosition = moveLimitation.GetPosition(newPosition);
                }
            }
            else
            {
                newPosition.z = 0;
            }

            return newPosition;
        }

        private Vector3 GetPlayerInputs(Vector3 newPosition)
        {
            newPosition.x += input.GetHoriozntal() * speed * Time.unscaledDeltaTime;
            newPosition.y += input.GetVertical() * speed * Time.unscaledDeltaTime;
            return newPosition;
        }

        private void OnDrawGizmosSelected()
        {
            moveLimitation.DrawGizmo(Color.green);
        }

    }
}