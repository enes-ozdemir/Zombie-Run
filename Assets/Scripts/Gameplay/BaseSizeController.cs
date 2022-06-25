using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class BaseSizeController : MonoBehaviour
    {
        public List<CharacterBase> characters = new();
        public int startCharacterSize = 5;
        public int currentCharacterSize;
        public string currentCharacterName = "Zombie";

        private void Start()
        {
            AddCharacter(startCharacterSize, currentCharacterName, true);
            Debug.Log($"currentCharacterName: {currentCharacterName}");
            currentCharacterSize = startCharacterSize;
        }

        public void RemoveCharacter(int size)
        {
            Debug.Log("Entered RemoveCharacter");

            for (var i = 0; i < size; i++)
            {
                //characters[i].animationController.PlayDeadAnim();
                ObjectPooler.Instance.KillCharacter(characters[i].gameObject);
                characters.RemoveAt(i);
                currentCharacterSize--;

                if (currentCharacterSize <= 0)
                {
                    //TODO: Game Over
                }

                //characters[i].gameObject.SetActive(false);
            }
        }

        protected void AddCharacter(int size, string charTag, bool isStart = false)
        {
            Debug.Log("Entered AddCharacter" + charTag);
            StartCoroutine(AddCharacterCoroutine(size, isStart, charTag));
        }

        private IEnumerator AddCharacterCoroutine(int size, bool isStart, string charTag)
        {
            Debug.Log("Entered AddCharacter" + charTag);
            currentCharacterSize += size;

            for (var i = 0; i < size; i++)
            {
                var position = transform.position;
                Vector3 pos = new Vector3(Random.Range
                        (position.x - 1, position.x + 1), position.y,
                    Random.Range(position.z - 1, position.z + 1));

                GameObject addedCharacter = ObjectPooler.Instance.SpawnCharacter(charTag, pos, Quaternion.identity);
                characters.Add(addedCharacter.GetComponent<CharacterBase>());
                addedCharacter.SetActive(true);
                addedCharacter.transform.localEulerAngles = Vector3.zero;
                addedCharacter.transform.position = pos;
                addedCharacter.transform.parent = transform;
                //addedCharacter.GetComponent<Character>().index = characters.Count;
                addedCharacter.transform.localPosition = Vector3.MoveTowards(addedCharacter.transform.localPosition,
                    position, 2f * Time.deltaTime);

                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}