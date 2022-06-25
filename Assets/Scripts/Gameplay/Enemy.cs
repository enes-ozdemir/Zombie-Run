using UnityEngine;

namespace Gameplay
{
    public class Enemy : CharacterBase
    {
        [SerializeField] private GameObject enemyPrefab;
        private BaseSizeController _baseSizeController;

        [SerializeField] private int startSize = 1;
        [SerializeField] private int enemyCount;


    }
}