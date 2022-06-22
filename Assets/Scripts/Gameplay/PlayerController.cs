using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public List<CharacterBase> characters = new List<CharacterBase>();
        public int startCharacterSize = 5;
        public int currentCharacterSize;
        private bool isFighting = false;
        [SerializeField] private GameObject playerCharacter;

        #region Singleton

        public static PlayerController instance;

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
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(0f, transform.position.y, transform.position.z),
                    100f * Time.deltaTime);
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag("Waypoint"))
            {
                Debug.Log("Enteed waypoint");

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

            for (int i = 0; i < size; i++)
            {
                characters[i].PlayDeadAnim();
                characters.RemoveAt(currentCharacterSize - 1);
                currentCharacterSize--;
                ObjectPooler.KillZombie(characters[i].gameObject);
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

            for (int i = 0; i < size; i++)
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
                yield return new WaitForSeconds(0.1f);
                //addedCharacter.GetComponent<Character>().index = characters.Count;
                addedCharacter.transform.localPosition = Vector3.MoveTowards(addedCharacter.transform.localPosition,
                    position, 2f * Time.deltaTime);
            }
        }


        // public void RemoveCharacter(int size)
        // {
        //     for(int i = 0; i < size; i++)
        //     {
        //         if(currentCharacterSize > 0)
        //         {
        //             characters[0].gameObject.SetActive(false);
        //             CharacterDeath(characters[0].index);
        //         }
        //     }
        // }
    }
}