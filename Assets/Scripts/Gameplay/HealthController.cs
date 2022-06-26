using System;
using System.Collections;
using System.Collections.Generic;
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
            currentHealth = maxHealth;
        }
        
        public void SetHealth(float amount)
        {
            healthBar.fillAmount = amount;
        }

        public void TakeDamage(int amount)
        {
            StartCoroutine(TakeDamageCoroutine(amount));
        }
        
        private IEnumerator TakeDamageCoroutine(int amount)
        {
            currentHealth -= amount;
            healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
            yield return new WaitForSeconds(0.1f);
            if (currentHealth <= 0)
            {
                healthCanvas.enabled = false;
                Destroy(gameObject);
            }
        }   

    }
}