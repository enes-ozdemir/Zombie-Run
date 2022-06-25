using System;
using UnityEngine;

namespace Gameplay
{
    public class Player : Character
    {
        public PlayerSizeController sizeController;
        [SerializeField] private GameObject enemyPrefab;

        private void OnValidate()
        {
        }
        
        
    }
}