using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public int startCharacterSize = 5;
        public int currentCharacterSize;
        private bool isFighting = false;
        [SerializeField] private GameObject playerCharacter;

        public float distBTWCharacters = 0.4f;

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
                transform.position =
                    Vector3.MoveTowards(transform.position,
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
                AddCharacter(25);
                //currentCharacterSize
            }
        }

        private void AddCharacter(int size)
        {
            Debug.Log("Entered AddCharacter");
            for (int i = 0; i < size; i++)
            {
                var position = transform.position;
                Vector3 pos = new Vector3(Random.Range
                        (position.x - 1, position.x + 1), position.y,
                    Random.Range(position.z - 1, position.z + 1));
                GameObject addedCharacter = Instantiate(playerCharacter, pos, Quaternion.identity);
                
                addedCharacter.SetActive(true);
                addedCharacter.transform.localEulerAngles = Vector3.zero;
                addedCharacter.transform.position = pos;
                addedCharacter.transform.parent = transform;
                //addedCharacter.GetComponent<Character>().index = characters.Count;
                currentCharacterSize++;
                addedCharacter.transform.localPosition = Vector3.MoveTowards(addedCharacter.transform.localPosition,
                    position, 2f * Time.deltaTime);
            }
        }
    }
}