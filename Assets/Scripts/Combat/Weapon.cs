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

        [SerializeField] float range = Mathf.Infinity;
        public float Range { get { return range; } }

        [SerializeField] AudioClip shootingAudio;
        public AudioClip ShootingAudio { get { return shootingAudio; } }

        GameObject player;

        private void Start() {
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
                hittedGameObject.GetComponent<EnemyHealth>().ProcessHit(damage);
            }
        }
    }
}

