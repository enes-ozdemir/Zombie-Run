using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject gameObject;
            public int size;
        }

        #region "Singleton"

        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        private Dictionary<string, Queue<GameObject>> _poolDictionary;
        public List<Pool> pools;

        private void Start()
        {
            SetPool();
        }

        private void SetPool()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (var i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.gameObject);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnZombie(string charTag, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(charTag)) return null;

            GameObject objectToSpawn = _poolDictionary[charTag].Dequeue();

            objectToSpawn.SetActive(transform);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            _poolDictionary[charTag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        public static void KillZombie(GameObject zombie)
        {
            zombie.SetActive(false);
        }
    }
}