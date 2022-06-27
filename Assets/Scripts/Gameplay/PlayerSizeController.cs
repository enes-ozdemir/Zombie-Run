using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PlayerSizeController : BaseSizeController
    {
        [SerializeField] public UnityEvent onEnemyEncounter;
        [SerializeField] private TextMeshPro countText;

        private void Start()
        {
            
            AddCharacter(startCharacterSize);
            currentCharacterSize = startCharacterSize;
            countText.text = currentCharacterSize.ToString();
        }
        
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
            switch (currentOperation)
            {
                case Waypoint.Operation.Add:
                    Debug.Log("Add");
                    AddCharacter(currentValue);
                    break;
                case Waypoint.Operation.Subtract:
                    Debug.Log("Subtract");
                    RemoveCharacter(currentValue);
                    currentCharacterSize -= currentValue;
                    break;
                case Waypoint.Operation.Multiply:
                    Debug.Log("Multiply");
                    AddCharacter(currentCharacterSize * currentValue - currentCharacterSize);
                    break;
                case Waypoint.Operation.Divide:
                    Debug.Log("Divide");
                    RemoveCharacter(currentCharacterSize - currentCharacterSize / currentValue);
                    break;
            }

            countText.text = currentCharacterSize.ToString();
        }
    }
}