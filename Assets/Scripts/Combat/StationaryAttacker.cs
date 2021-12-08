using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Core;

namespace Shooter.Combat
{
    [RequireComponent(typeof(Health))]
    public class StationaryAttacker : MonoBehaviour
    {
        // Config parameters
        [Tooltip("Rotation in degrees/sec")]
        [SerializeField] float scanningRotationSpeed = 50f;
        [SerializeField] float lockingRotationMultiplier = 2f;
        [SerializeField] GameObject scanningObject;
        [SerializeField] float attackDistance = 10f;
        [SerializeField] float delayBetweenLockingAndScan = 3f;
        [SerializeField] ParticleSystem shootParticlse;

        // Cached references
        Transform playerPos = null;

        // stats
        bool isPlayerInAttackRange = false;
        float timeFromLastTargetedPlayer = Mathf.Infinity;
        
        void Start()
        {
            playerPos = GameObject.FindWithTag("Player").transform;          
        }

        void Update()
        {
            timeFromLastTargetedPlayer += Time.deltaTime;

            if (IsPlayerInRange())
            {
                shootParticlse.Play();
                LookAtPlayer();
                ShootSequence();
                timeFromLastTargetedPlayer = 0f;
            }
            else
            {
                StopShooting();
                RotationScan();
            }
        }

        private bool IsPlayerInRange()
        {
            isPlayerInAttackRange = Vector3.Distance(transform.position, playerPos.position) < attackDistance;
            return isPlayerInAttackRange;
        }

        private void LookAtPlayer()
        {
            scanningObject.transform.LookAt(playerPos);
        }

        private void ShootSequence()
        {
            var emissionModule = shootParticlse.emission;
            emissionModule.enabled = true; 
        }

        private void StopShooting()
        {
            var emissionModule = shootParticlse.emission;
            emissionModule.enabled = false;
        }

        private void RotationScan()
        {
            if (scanningObject != null && timeFromLastTargetedPlayer > delayBetweenLockingAndScan)
            {
               scanningObject.transform.Rotate(Vector3.up, scanningRotationSpeed * Time.deltaTime, Space.Self);
            }
        }
        
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}


