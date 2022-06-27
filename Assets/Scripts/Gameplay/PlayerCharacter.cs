using UnityEngine;

namespace Gameplay
{
    public class PlayerCharacter : CharacterBase
    {
        private void Start()
        {
            animationController.PlayBornAnim();
            transform.GetChild(characterMeshIndex).gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.transform.CompareTag("Obstacle"))
            {
                OnBecameInvisible();
                animationController.PlayKnockBackAnim();
            }
        }
    }
}