using System;
using UnityEngine;

namespace Gameplay
{
    public class AnimationController : MonoBehaviour
    {
        private Animator _animation;
        
        private void Awake()
        {
            _animation.GetComponent<Animator>();
        }

        public void PlayDeadAnim()
        {
            Debug.Log("Dead");
            //TODO : Play dead animation
        }  
        
        public void PlayBornAnim()
        {
            Debug.Log("Born");
            //TODO : Play born animation
        }
    }
}