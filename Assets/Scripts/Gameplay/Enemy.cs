using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class Enemy : Character
    {
        [SerializeField] private GameObject enemyPrefab;
         public BaseSizeController sizeController;
         public EnemyType enemyType;

        
        public enum EnemyType {
            Human,
            Police,
            Boss
        }
    }
}