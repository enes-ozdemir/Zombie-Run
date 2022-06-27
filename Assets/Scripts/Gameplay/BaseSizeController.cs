using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BaseSizeController : MonoBehaviour
    {
        [SerializeField] public List<CharacterBase> characters = new();
        [SerializeField] public int startCharacterSize = 5;
        [SerializeField] public int currentCharacterSize;
        private CharacterPool _characterPool;

        private void Awake()
        {
            _characterPool = GetComponent<CharacterPool>();
        }

        private void Start()
        {
          //  int randomMesh = Random.Range(1, Constant.maxMeshIndex);
            AddCharacter(startCharacterSize);
            currentCharacterSize = startCharacterSize;
        }

        public void RemoveCharacter(int size)
        {
            Debug.Log("Entered RemoveCharacter " + size);

            for (var i = 0; i < size; i++)
            {
                //characters[i].animationController.PlayDeadAnim();
                _characterPool.characterPool.Release(characters[i]);

                characters.RemoveAt(i);
                currentCharacterSize--;

                if (currentCharacterSize <= 0)
                {
                    //TODO: Game Over
                }
            }
        }

        public void AddCharacter(int size)
        {
            Debug.Log("Entered AddCharacter");
            StartCoroutine(AddCharacterCoroutine(size));
        }

        private IEnumerator AddCharacterCoroutine(int size)
        {
            Debug.Log("AddCharacter called");
            currentCharacterSize += size;
            for (var i = 0; i < size; i++)
            {
                if (_characterPool != null)
                {
                    //_characterPool.characterMeshIndex = newCharMeshIndex;
                    _characterPool.characterPool.Get();
                }

                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}