using UnityEngine;

namespace Gameplay
{
    public class PlayerCharacter : CharacterBase
    {
        [Range(1, 51)] public int characterMeshIndex = 1;
        private void Start()
        {
            animationController.PlayBornAnim();
            characterMeshIndex = Random.Range(0, 51);
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