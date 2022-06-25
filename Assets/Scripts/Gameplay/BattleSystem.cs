using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class BattleSystem : MonoBehaviour
    {
        public void Battle(BaseSizeController enemySizeController,BaseSizeController playerSizeController)
        {
            StartCoroutine(BattleCoroutine(enemySizeController,playerSizeController));
        }

        private IEnumerator BattleCoroutine(BaseSizeController enemySizeController,BaseSizeController playerSizeController)
        {
            while (enemySizeController.currentCharacterSize > 0 && playerSizeController.currentCharacterSize > 0)
            {
                enemySizeController.RemoveCharacter(1);
                playerSizeController.RemoveCharacter(1);

                var enemySize = enemySizeController.currentCharacterSize;
                var playerSize = playerSizeController.currentCharacterSize;
                Debug.Log($"Enemy size: {enemySize} , Player size: {playerSize}");

                //todo battle animation for both

                yield return new WaitForSeconds(0.1f);
            }

            if (enemySizeController.currentCharacterSize == 0)
            {
                Debug.Log("Player wins");
                (playerSizeController as PlayerSizeController)?.onEnemyEncounter.Invoke();
                Destroy(this);
            }
            else
            {
                Debug.Log("Enemy wins");
                Destroy(this);
                //game over scene
            }
        }
    }
}