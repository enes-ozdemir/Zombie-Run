using UnityEngine;

namespace Gameplay
{
    public abstract class CharacterBase : MonoBehaviour
    {
        public bool isDead = false;
        public bool canMove = false;
        public Transform target;
        private Vector3 _characterDirection;
        private bool _isJumping = false;
        private Rigidbody _rb;

        public void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public abstract void CharacterDie();

        private void FixedUpdate()
        {
            _characterDirection = (target.position - transform.position).normalized;
            _rb.AddForce(_characterDirection * Vector3.Distance(target.position, transform.position) * 55f, ForceMode.Force);

        }
    }
}