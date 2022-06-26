using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay
{
    public class CharacterBase : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _characterDirection;
        private Rigidbody _rb;

        public IObjectPool<CharacterBase> _characterPool;

        public void SetPool(IObjectPool<CharacterBase> pool) => _characterPool = pool;

        public void OnBecameInvisible() => _characterPool.Release(this);

        public void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _target = transform.parent.transform;
            Debug.Log("Target name" + _target.name);
        }

        private void FixedUpdate()
        {
            _characterDirection = (_target.position - transform.position).normalized;
            _rb.AddForce(_characterDirection * Vector3.Distance(_target.position, transform.position) * 55f,
                ForceMode.Force);
        }
    }
}