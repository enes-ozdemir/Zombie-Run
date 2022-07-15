using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class CharacterPool : MonoBehaviour
    {
        public IObjectPool<CharacterBase> characterPool;
        [SerializeField] private CharacterBase characterPrefab;
        private BaseSizeController _sizeController;
       // [Range(1, 51)] public int characterMeshIndex = 1;

        void Awake()
        {
            _sizeController = GetComponent<BaseSizeController>();
            characterPool = new ObjectPool<CharacterBase>(CreateCharacter, OnGet, OnRelease);
        }

        private CharacterBase CreateCharacter()
        {
            var character = Instantiate(characterPrefab, transform);
            _sizeController.characters.Add(character);
            character.SetPool(characterPool);
            return character;
        }

        private void OnRelease(CharacterBase characterBase)
        {
            var characterDie = characterBase.CharacterDie();
            StartCoroutine(characterDie);
        }

        private void OnGet(CharacterBase characterBase)
        {
            characterBase.gameObject.SetActive(true);
            //characterBase.characterMeshIndex = characterMeshIndex;
            var characterTransform = characterBase.transform;
            var position = transform.position;

            Vector3 pos = new Vector3(Random.Range
                    (position.x - 1, position.x + 1), position.y,
                Random.Range(position.z - 1, position.z + 1));

            characterTransform.position = pos;
            characterTransform.localEulerAngles = Vector3.zero;
           // characterTransform.parent = characterTransform;

            characterBase.transform.localPosition = Vector3.MoveTowards(characterTransform.localPosition,
                characterTransform.position, 2f * Time.deltaTime);
        }
    }
}