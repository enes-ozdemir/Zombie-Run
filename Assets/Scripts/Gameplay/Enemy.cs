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

        private void Awake()
        {
        }
    }
}