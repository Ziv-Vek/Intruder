using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Core;

namespace Shooter.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] WeaponType weaponType;
        public WeaponType GetWeaponType { get { return weaponType; } }
        
        [SerializeField] int damage = 20;
        public int Damage { get { return damage; } }

        [SerializeField] float range = Mathf.Infinity;
        public float Range { get { return range; } }

        [SerializeField] AudioClip shootingAudio;
        public AudioClip ShootingAudio { get { return shootingAudio; } }

        GameObject player;

        private void Awake() {
            player = GameObject.FindWithTag("Player");
        }
    
        private void OnParticleCollision(GameObject hittedGameObject)
        {
            if (hittedGameObject.GetComponent<Health>() == null)
            {
                return;
            }
            else if (hittedGameObject.tag == "Player")
            {
                hittedGameObject.GetComponent<PlayerHealthManager>().ProcessHit(damage);
            }
            else
            {
                if (hittedGameObject.GetComponent<EnemyHealth>())
                {
                    hittedGameObject.GetComponent<EnemyHealth>().ProcessHit(damage);
                }
                if (hittedGameObject.GetComponent<Health>())
                {
                    hittedGameObject.GetComponent<Health>().TakeDamage(damage);
                }
            }
        }
    }
}

