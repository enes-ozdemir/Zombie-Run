using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] public Image healthBar;
        [SerializeField] public Canvas healthCanvas;
        public int currentHealth;
        public int maxHealth;

        private void Start()
        {
            var boss = ((Boss)GetComponent<EnemyCharacter>().enemy);
            maxHealth = boss.maxHealth;
            currentHealth = maxHealth;
        }

        private void SetHealth(float amount)
        {
            Debug.Log("Health changed");
            healthBar.fillAmount = amount;
        }

        public void TakeDamage(int amount)
        {
            //StartCoroutine(TakeDamageCoroutine(amount));
            TakeDamageCoroutine(amount);
        }
        
        private void TakeDamageCoroutine(int amount)
        {
            currentHealth -= amount;
            SetHealth((float) currentHealth / maxHealth);

            if (currentHealth <= 0)
            {
                healthCanvas.enabled = false;
                transform.gameObject.SetActive(false);
            }
        }   

    }
}