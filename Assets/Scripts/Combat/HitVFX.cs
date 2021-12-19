using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Core;

namespace Shooter.Combat
{
    public class HitVFX : MonoBehaviour
    {
        [SerializeField] ParticleSystem debrisParticle;

        private void OnParticleCollision(GameObject hittingPariticle) 
        {
            debrisParticle.Play();
            
            var currentHealth = GetComponent<Health>().GetCurrentHealth;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

