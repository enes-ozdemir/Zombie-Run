using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class Boss : Enemy
    {

        public int damage;
        public int currentHealth;
        public int maxHealth;
        public int attackSpeed;
        public int attackRange;
        public HealthController _healthController;
      
    }
}

// [CreateAssetMenu]
// public class BossCharacter : ScriptableObject
// {
//     public Enemy enemy;
//     public int damage;
//     public int health;
//     public int attackSpeed;
//     public int attackRange;
//
// }