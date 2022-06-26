using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class BattleSystem : MonoBehaviour
    {
        public void Battle(BaseSizeController enemySizeController,BaseSizeController playerSizeController)
        {
            Debug.Log($"Battle ");
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
                //game over scene
            }
            Destroy(this);
        }

        public void EatHuman(BaseSizeController enemySizeController,BaseSizeController playerSizeController)
        {
            Debug.Log($"EatHuman ");
            StartCoroutine(EatHumanCoroutine(enemySizeController,playerSizeController));
        }
        
        private IEnumerator EatHumanCoroutine(BaseSizeController enemySizeController,BaseSizeController playerSizeController)
        {
            while (enemySizeController.currentCharacterSize > 0)
            {
                enemySizeController.RemoveCharacter(1);
                var enemySize = enemySizeController.currentCharacterSize;
                Debug.Log($"Enemy size: {enemySize}");

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
                //game over scene
            }
            Destroy(this);
        }

        public void BossBattle(HealthController enemySize, PlayerSizeController playerSizeController)
        {
            StartCoroutine(BossBattleCoroutine(enemySize,playerSizeController));
        }

        private IEnumerator BossBattleCoroutine(HealthController enemyHealthController, PlayerSizeController playerSizeController)
        {
            while (enemyHealthController.currentHealth > 0 && playerSizeController.currentCharacterSize > 0)
            {
                                
                playerSizeController.RemoveCharacter(1);
                enemyHealthController.TakeDamage(1);

                yield return new WaitForSeconds(0.1f);
            }
            
        }
    }
}