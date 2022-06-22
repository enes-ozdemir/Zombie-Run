using UnityEngine;

namespace Gameplay
{
    public class CharacterBase : MonoBehaviour
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
            target = transform.parent.GetComponentInParent<Transform>();
        }
        
        private void FixedUpdate()
        {
            _characterDirection = (target.position - transform.position).normalized;
            _rb.AddForce(_characterDirection * Vector3.Distance(target.position, transform.position) * 55f, ForceMode.Force);
        }

        public void PlayDeadAnim()
        {
            Debug.Log("Dead");
        }
        
    }
}