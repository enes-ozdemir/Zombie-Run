using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        [FormerlySerializedAs("distance")] [SerializeField]
        private Vector3 distanceToTarget;

        [FormerlySerializedAs("camSpeed")] [SerializeField]
        private float cameraSpeed;

        private void LateUpdate()
        {
            transform.position =
                Vector3.Lerp(transform.position,
                    target.transform.position + distanceToTarget, cameraSpeed * Time.deltaTime);
        }
    }
}