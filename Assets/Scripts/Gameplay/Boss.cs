
namespace Gameplay
{
    public class Boss : Enemy
    {

        public int damage;
        public int health;
        public int attackSpeed;
        public int attackRange;
        private HealthController _healthController;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
            health= _healthController.maxHealth;
        }
    }
}