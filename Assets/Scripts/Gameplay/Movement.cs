using UnityEngine;

namespace Gameplay
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10f;
        private Rigidbody _rigidbody;
        private PlayerController _playerController;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerController = GetComponent<PlayerController>();
        }

        void FixedUpdate()
        {
            if(!_playerController.canMove) return;
            
            var position = transform.position;
            position = Vector3.MoveTowards(position, position + Vector3.forward, movementSpeed * Time.deltaTime);
            transform.position = position;
            _rigidbody.AddForce(new Vector3(0, 0, movementSpeed * Time.deltaTime));
        }
    }
}