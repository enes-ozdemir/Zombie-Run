using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PlayerSizeController : BaseSizeController
    {
        [SerializeField] public UnityEvent onEnemyEncounter;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag("Waypoint"))
            {
                Debug.Log("Entered to a waypoint");

                var currentOperation = collision.GetComponent<Waypoint>().operation;
                var currentValue = collision.GetComponent<Waypoint>().value;

                SetNewCharacterSize(currentOperation, currentValue);
            }

            if (collision.transform.CompareTag("Enemy"))
            {
                onEnemyEncounter.Invoke();
                var enemySize = collision.GetComponentInParent<BaseSizeController>();
                if (enemySize == null)
                {
                    Debug.LogError("Enemy is null");
                }
                else
                {
                    var enemy = collision.GetComponent<EnemyCharacter>();
                    Debug.Log("BattleSystem is called");

                    switch (enemy.enemy.enemyType)
                    {
                        case Enemy.EnemyType.Police:
                            Debug.Log("Enemy type is Police");
                            gameObject.AddComponent<BattleSystem>().Battle(enemySize, this);
                            break;
                        case Enemy.EnemyType.Human:
                            Debug.Log("Enemy type is Human");
                            gameObject.AddComponent<BattleSystem>().EatHuman(enemySize, this);
                            break;
                        case Enemy.EnemyType.Boss:
                            Debug.Log("Enemy type is Boss");
                            var enemyCharacter = collision.GetComponent<EnemyCharacter>();
                            gameObject.AddComponent<BattleSystem>().BossBattle(enemyCharacter, this);
                            break;
                    }
                }
            }
        }

        private void SetNewCharacterSize(Waypoint.Operation currentOperation, int currentValue)
        {
            int randomMesh = Random.Range(1, Constant.maxMeshIndex);
            switch (currentOperation)
            {
                case Waypoint.Operation.Add:
                    AddCharacter(currentValue, randomMesh);
                    break;
                case Waypoint.Operation.Subtract:
                    RemoveCharacter(currentValue);
                    currentCharacterSize -= currentValue;
                    break;
                case Waypoint.Operation.Multiply:
                    AddCharacter(currentCharacterSize * currentValue - currentCharacterSize,randomMesh);
                    break;
                case Waypoint.Operation.Divide:
                    RemoveCharacter(currentCharacterSize - currentCharacterSize / currentValue);
                    break;
            }
        }
    }
}