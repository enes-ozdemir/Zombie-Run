using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class BattleSystem : MonoBehaviour
    {
        public void Battle(BaseSizeController enemySizeController, BaseSizeController playerSizeController)
        {
            Debug.Log($"Battle ");
            StartCoroutine(BattleCoroutine(enemySizeController, playerSizeController));
        }

        private IEnumerator BattleCoroutine(BaseSizeController enemySizeController,
            BaseSizeController playerSizeController)
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

        public void EatHuman(BaseSizeController enemySizeController, BaseSizeController playerSizeController)
        {
            Debug.Log($"EatHuman ");
            StartCoroutine(EatHumanCoroutine(enemySizeController, playerSizeController));
        }

        private IEnumerator EatHumanCoroutine(BaseSizeController enemySizeController,
            BaseSizeController playerSizeController)
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

        public void BossBattle(EnemyCharacter enemyCharacter, PlayerSizeController playerSizeController)
        {
            Debug.Log("Entered boss battle");
            StartCoroutine(BossBattleCoroutine(enemyCharacter, playerSizeController));
        }

        private IEnumerator BossBattleCoroutine(EnemyCharacter enemyCharacter,
            PlayerSizeController playerSizeController)
        {
            var boss = (Boss) enemyCharacter.enemy;
            while (boss.currentHealth > 0 && playerSizeController.currentCharacterSize > 0)
            {
                playerSizeController.RemoveCharacter(1);
                //Todo we can seperate this method according the boss and player attack speed
                boss._healthController.TakeDamage(1);
                if (boss.currentHealth < 0)
                {
                    enemyCharacter._characterPool.Release(enemyCharacter);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}