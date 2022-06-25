using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay
{
    public class Character : MonoBehaviour
    {
        protected Transform target;
        private Vector3 _characterDirection;
        private Rigidbody _rb;

        private IObjectPool<Character> _characterPool;

        public void SetPool(IObjectPool<Character> pool) => _characterPool = pool;

        public void OnBecameInvisible() => _characterPool.Release(this);

        public void Start()
        {
            _rb = GetComponent<Rigidbody>();
            target = transform.parent.transform;
            Debug.Log("Target name" + target.name);
        }

        private void FixedUpdate()
        {
            _characterDirection = (target.position - transform.position).normalized;
            _rb.AddForce(_characterDirection * Vector3.Distance(target.position, transform.position) * 55f,
                ForceMode.Force);
        }
    }
}