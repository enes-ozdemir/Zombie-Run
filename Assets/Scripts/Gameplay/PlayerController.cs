using System.Collections;
using UnityEngine;


namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public bool canMove = true;
        private PlayerSizeController _playerSizeController;


        private void Start()
        {
            _playerSizeController = GetComponent<PlayerSizeController>();
        }

        public void ChangeMovementState() => canMove = !canMove;
        public void EnemyEncounter() => canMove = false;


        public IEnumerator MoveCenter()
        {
            Debug.Log("Entered MoveCenter");
            while (transform.position != new Vector3(0f, transform.position.y, transform.position.z))
            {
                var position = transform.position;
                position = Vector3.MoveTowards(position,
                    new Vector3(0f, position.y, position.z),
                    100f * Time.deltaTime);

                transform.position = position;
                yield return null;
            }
        }
    }
}