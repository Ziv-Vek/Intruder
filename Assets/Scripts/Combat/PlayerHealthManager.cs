using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Core;
using System;
using Shooter.Controls;

namespace Shooter.Combat
{
    public class PlayerHealthManager : MonoBehaviour
    {
        // config
        [SerializeField] AudioClip hitSFX;
        [Tooltip("Delay in sec before level restart after player died")]
        [SerializeField] float delayLevelRestart = 1f;

        // cached refernces
        AudioSource myAudioSource;

        // stats
        bool isAlive = true;
        
        private void Start() {
            myAudioSource = GetComponent<AudioSource>();
        }

        public void ProcessHit(int damage)
        {
            Debug.Log("collision from :" + gameObject.name);
            GetComponent<Health>().TakeDamage(damage);
            myAudioSource.PlayOneShot(hitSFX);
            int currentHealth = GetComponent<Health>().GetCurrentHealth;
            if (currentHealth <= 0)
            {
                if (isAlive == true)
                {
                    DieSequence();
                }
            }
        }

        private void DieSequence()
        {
            isAlive = false;
            GetComponent<Movement>().EnableControls = false;
            // To Do: disable FP camera + move to 3rd person camera + look at player + move on cart around player
            // To Do: do big explosion VFX of the ship
            // To Do: do big explosion SFX

            StartCoroutine("RestartLevel");
            // after a time delay: restart scene - instantiate the player in saved position
            // after a time delay: enable player controls
        }

        IEnumerator RestartLevel()
        {
            yield return new WaitForSecondsRealtime(delayLevelRestart);
            FindObjectOfType<GameSession>().RestartScene();
        }
    }
}

