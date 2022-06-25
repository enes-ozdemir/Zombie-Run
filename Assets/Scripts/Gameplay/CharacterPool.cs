using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class CharacterPool : MonoBehaviour
    {
        public const string Tag = "CharacterPool";
        public IObjectPool<Character> _characterPool;
        [SerializeField] private Character characterPrefab;
        [SerializeField] private BaseSizeController sizeController;

        void Awake()
        {
            sizeController = GetComponent<BaseSizeController>();
            _characterPool = new ObjectPool<Character>(CreateCharacter, OnGet, OnRelease);
        }

        private Character CreateCharacter()
        {
            Debug.Log("Create Character called");
            var character = Instantiate(characterPrefab, transform);
            sizeController.characters.Add(character);
            character.SetPool(_characterPool);
            return character;
        }

        private void OnRelease(Character character)
        {
            Debug.Log("Create Character called",this);

            character.gameObject.SetActive(false);
        }

        private void OnGet(Character character)
        {
            Debug.Log("Entered OnGet");

            character.gameObject.SetActive(true);
            var characterTransform = character.transform;
            var position = transform.position;

            Vector3 pos = new Vector3(Random.Range
                    (position.x - 1, position.x + 1), position.y,
                Random.Range(position.z - 1, position.z + 1));

            characterTransform.position = pos;
            characterTransform.localEulerAngles = Vector3.zero;
            characterTransform.parent = characterTransform;

            character.transform.localPosition = Vector3.MoveTowards(characterTransform.localPosition,
                characterTransform.position, 2f * Time.deltaTime);
        }
    }
}