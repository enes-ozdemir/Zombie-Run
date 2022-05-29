using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private Vector3 distance;
        [SerializeField] private float camSpeed;


        private void LateUpdate()
        {
            transform.position =
                Vector3.Lerp(transform.position,
                    target.transform.position + distance, camSpeed * Time.deltaTime);
        }
    }
}   