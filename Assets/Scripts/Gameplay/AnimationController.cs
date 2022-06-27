using UnityEngine;

namespace Gameplay
{
    public class AnimationController : MonoBehaviour
    {
        private Animator _animation;

        private void Awake()
        {
            _animation = GetComponent<Animator>();
           
        }

        public void PlayDeadAnim()
        {
            Debug.Log("Dead");
            _animation.Play("Death");
        }

        public void PlayBornAnim()
        {
            Debug.Log("Born");
            _animation.Play("Born");
        }
        public void PlayAttackAnim()
        {
            Debug.Log("Attack1");
            _animation.Play("Attack1");
        }

        public void PlayWalkAnim()
        {
            Debug.Log("Walk");
            _animation.Play("Walk");
        }

        public void PlayBossAttackAnim()
        {
            Debug.Log("BossAttack");
            _animation.Play("BossAttack");
        }

        public void PlayKnockBackAnim()
        {
            Debug.Log("KnockBack");
            _animation.Play("KnockBack");
        }

        public void PlayIdleAnim()
        {
            Debug.Log("Idle");
            _animation.Play("Idle");
        }
    }
}