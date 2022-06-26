using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class Enemy : ScriptableObject
    {
        public EnemyType enemyType;

        public enum EnemyType
        {
            Human,
            Police,
            Boss,
        }
    }
}