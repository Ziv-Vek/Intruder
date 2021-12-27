using UnityEngine;

namespace Shooter.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;
        public int GetMaxHealth { get { return maxHealth; } }

        [SerializeField] int currentHealth;
        public int GetCurrentHealth { get { return currentHealth; } }

        private void Start() {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
        }

    }
}

