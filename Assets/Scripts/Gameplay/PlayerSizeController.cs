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
            CheckWaypointCollision(collision);

            CheckEnemyCollision(collision);
        }

        private void CheckEnemyCollision(Collider collision)
        {
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

        private void CheckWaypointCollision(Collider collision)
        {
            var currentWaypoint = collision.GetComponentInParent<Waypoint>();

            if (collision.transform.CompareTag("WaypointLeft") && !currentWaypoint.isActive)
            {
                Debug.Log("Entered to a WaypointLeft");
                var currentOperation = currentWaypoint.leftOperation;
                var currentValue = currentWaypoint.leftValue;
                currentWaypoint.isActive = true;
                SetNewCharacterSize(currentOperation, currentValue);
            }
            else if (collision.transform.CompareTag("WaypointRight") && !currentWaypoint.isActive)
            {
                Debug.Log("Entered to a WaypointRight");
                var currentOperation = currentWaypoint.rightOperation;
                var currentValue = currentWaypoint.rightValue;
                currentWaypoint.isActive = true;
                SetNewCharacterSize(currentOperation, currentValue);
            }
        }

        private void SetNewCharacterSize(Waypoint.Operation currentOperation, int currentValue)
        {
            Debug.Log(
                "Entered to a SetNewCharacterSize current Operation is" + currentOperation + "with" + currentValue);
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