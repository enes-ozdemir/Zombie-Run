using UnityEngine;

namespace Gameplay
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10f;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            var position = transform.position;
            position = Vector3.MoveTowards(position, position + Vector3.forward, movementSpeed * Time.deltaTime);
            transform.position = position;
            _rigidbody.AddForce(new Vector3(0, 0, movementSpeed * Time.deltaTime));
        }
    }
}