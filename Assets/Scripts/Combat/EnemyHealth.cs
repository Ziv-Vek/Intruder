using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Core;

namespace Shooter.Combat
{
    public class EnemyHealth : MonoBehaviour
    {
        public void ProcessHit(int damage)
        {
            GetComponent<Health>().TakeDamage(damage);
            int currentHealth = GetComponent<Health>().GetCurrentHealth;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

}

