using UnityEngine;

namespace Gameplay
{
    public class PlayerCharacter : CharacterBase
    {
        public GameObject playerPrefab;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.transform.CompareTag("Obstacle"))
            {
                OnBecameInvisible();
            }
        }
    }
}