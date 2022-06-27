using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay
{
    public class CharacterBase : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _characterDirection;
        private Rigidbody _rb;
        protected AnimationController animationController;
        //[Range(1, 51)] public int characterMeshIndex = 1;

        public IObjectPool<CharacterBase> characterPool;

        public void SetPool(IObjectPool<CharacterBase> pool) => characterPool = pool;

        private void Awake()
        {
            animationController = GetComponent<AnimationController>();
            _rb = GetComponent<Rigidbody>();
            _target = transform.parent.transform;
            Debug.Log("Target name" + _target.name);
        }

        public void OnBecameInvisible()
        {
            characterPool.Release(this);
        }


        private void FixedUpdate()
        {
            _characterDirection = (_target.position - transform.position).normalized;
            _rb.AddForce(_characterDirection * Vector3.Distance(_target.position, transform.position) * 55f,
                ForceMode.Force);
        }

        public void CharacterAttack()
        {
            animationController.PlayAttackAnim();
            //todo add particle effect to here
        }

        public IEnumerator CharacterDie()
        {
            animationController.PlayDeadAnim();

            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);

            //todo add particle effect to here
        }
    }
}