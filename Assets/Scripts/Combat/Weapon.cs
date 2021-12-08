using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Core;

namespace Shooter.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] int damage = 20;
        public int Damage { get { return damage; } }


        // private void OnParticleCollision(GameObject hittedGameObject)
        // {
        //     if (hittedGameObject.GetComponent<Health>() == null)
        //     {
        //         return;
        //     }
        //     else if (hittedGameObject.tag == "Player")
        //     {

        //     }
        //     else
        //     {

        //     }


        //     int damage = hittingParticle.GetComponent<Weapon>().Damage;
        //     GetComponent<Health>().TakeDamage(damage);
        //     int currentHealth = GetComponent<Health>().GetCurrentHealth;
        // }

    }
}

