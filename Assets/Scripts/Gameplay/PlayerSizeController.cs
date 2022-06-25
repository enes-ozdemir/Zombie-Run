using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PlayerSizeController : BaseSizeController
    {
        // #region Singleton
        //
        // public static PlayerSizeController instance;
        //
        // private void Awake()
        // {
        //     instance = this;
        // }
        //
        // #endregion

        [SerializeField] private UnityEvent onEnemyEncounter;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag("Waypoint"))
            {
                Debug.Log("Entered to a waypoint");

                var currentOperation = collision.GetComponent<Waypoint>().operation;
                var currentValue = collision.GetComponent<Waypoint>().value;

                SetNewCharacterSize(currentOperation, currentValue, "Zombie");
            }

            if (collision.transform.CompareTag("Enemy"))
            {
                onEnemyEncounter.Invoke();
                var enemySize = collision.GetComponentInParent<BaseSizeController>();
                if (enemySize == null)
                {
                    Debug.LogError("Enemy is null");
                }

                Battle(enemySize);
            }
        }

        private void Battle(BaseSizeController enemySizeController)
        {
            StartCoroutine(BattleCoroutine(enemySizeController));
        }

        private IEnumerator BattleCoroutine(BaseSizeController enemySizeController)
        {
            Debug.Log($"Enemy size: {enemySizeController.currentCharacterSize} , Player size: {currentCharacterSize}");
            while (enemySizeController.currentCharacterSize > 0 && currentCharacterSize > 0)
            {
                //todo battle animation for both
                RemoveCharacter(1);
                enemySizeController.RemoveCharacter(1);
                yield return new WaitForSeconds(0.1f);
            }

            if (currentCharacterSize == 0)
            {
                //todo gameover
            }
            else
            {
                onEnemyEncounter.Invoke();
            }
        }


        private void SetNewCharacterSize(Waypoint.Operation currentOperation, int currentValue, string charTag)
        {
            switch (currentOperation)
            {
                case Waypoint.Operation.Add:
                    AddCharacter(currentValue, charTag);
                    break;
                case Waypoint.Operation.Subtract:
                    RemoveCharacter(currentValue);
                    currentCharacterSize -= currentValue;
                    break;
                case Waypoint.Operation.Multiply:
                    AddCharacter(currentCharacterSize * currentValue - currentCharacterSize, charTag);
                    break;
                case Waypoint.Operation.Divide:
                    RemoveCharacter(currentCharacterSize - currentCharacterSize / currentValue);
                    break;
            }
        }
    }
}