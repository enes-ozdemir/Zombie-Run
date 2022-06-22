using UnityEngine;

namespace Gameplay
{
    public class CharacterBase : MonoBehaviour
    {
        [SerializeField] private bool canMove = true;
        private Transform _target;
        private Vector3 _characterDirection;
        private Rigidbody _rb;
        private GameObject _gameObject;

        public void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _target = transform.parent.GetComponentInParent<Transform>();
        }

        private void FixedUpdate()
        {
            _characterDirection = (_target.position - transform.position).normalized;
            _rb.AddForce(_characterDirection * Vector3.Distance(_target.position, transform.position) * 55f,
                ForceMode.Force);
        }
    }
}