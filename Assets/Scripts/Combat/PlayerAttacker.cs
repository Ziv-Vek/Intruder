using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Controls;
using Shooter.Core;
using System;

namespace Shooter.Combat
{
    public class PlayerAttacker : MonoBehaviour
    {
        // Config
        [SerializeField] ParticleSystem activeWeaponParticle;
        
        // Cached references
        PlayerInput myInput;

        void Start()
        {
            myInput = GetComponent<PlayerInput>();
            activeWeaponParticle.Play();
        }

        void Update()
        {
            Fire(myInput.fire);
        }

        private void Fire(bool enableShooting)
        {
            var weaponEmission = activeWeaponParticle.emission;
            weaponEmission.enabled = enableShooting;
        }
    }
}

