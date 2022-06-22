using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PlayerSizeController : MonoBehaviour
    {
        public List<CharacterBase> characters = new();
        public int startCharacterSize = 5;
        public int currentCharacterSize;

        #region Singleton

        public static PlayerSizeController instance;

        private void Awake()
        {
            instance = this;
        }

        #endregion

        private void Start()
        {
            AddCharacter(startCharacterSize);
            currentCharacterSize = startCharacterSize;
        }

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

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag("Waypoint"))
            {
                Debug.Log("Entered to a waypoint");

                var currentOperation = collision.GetComponent<Waypoint>().operation;
                var currentValue = collision.GetComponent<Waypoint>().value;

                SetNewCharacterSize(currentOperation, currentValue);
            }
        }

        private void SetNewCharacterSize(Waypoint.Operation currentOperation, int currentValue)
        {
            switch (currentOperation)
            {
                case Waypoint.Operation.Add:
                    AddCharacter(currentValue);
                    break;
                case Waypoint.Operation.Subtract:
                    RemoveCharacter(currentValue);
                    currentCharacterSize -= currentValue;
                    break;
                case Waypoint.Operation.Multiply:
                    AddCharacter(currentCharacterSize * currentValue - currentCharacterSize);
                    break;
                case Waypoint.Operation.Divide:
                    RemoveCharacter(currentCharacterSize - currentCharacterSize / currentValue);
                    break;
            }
        }

        private void RemoveCharacter(int size)
        {
            Debug.Log("Entered RemoveCharacter");

            for (var i = 0; i < size; i++)
            {
                characters[i].PlayDeadAnim();
                characters.RemoveAt(currentCharacterSize - 1);
                currentCharacterSize--;
                ObjectPooler.KillZombie(characters[i].gameObject);
                
                if(currentCharacterSize<=0)
                {
                    //TODO: Game Over
                }
                
                //characters[i].gameObject.SetActive(false);
            }
        }

        private void AddCharacter(int size)
        {
            Debug.Log("Entered AddCharacter");
            StartCoroutine(AddCharacterCoroutine(size));
        }

        private IEnumerator AddCharacterCoroutine(int size)
        {
            Debug.Log("Entered AddCharacter");
            currentCharacterSize += size;

            for (var i = 0; i < size; i++)
            {
                var position = transform.position;
                Vector3 pos = new Vector3(Random.Range
                        (position.x - 1, position.x + 1), position.y,
                    Random.Range(position.z - 1, position.z + 1));

                GameObject addedCharacter = ObjectPooler.Instance.SpawnZombie("Zombie", pos, Quaternion.identity);
                characters.Add(addedCharacter.GetComponent<CharacterBase>());
                addedCharacter.SetActive(true);
                addedCharacter.transform.localEulerAngles = Vector3.zero;
                addedCharacter.transform.position = pos;
                addedCharacter.transform.parent = transform;
                //addedCharacter.GetComponent<Character>().index = characters.Count;
                addedCharacter.transform.localPosition = Vector3.MoveTowards(addedCharacter.transform.localPosition,
                    position, 2f * Time.deltaTime);
                
                yield return new WaitForSeconds(0.1f);

            }
        }
    }
}