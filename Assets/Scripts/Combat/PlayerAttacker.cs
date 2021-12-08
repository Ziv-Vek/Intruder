using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Controls;
using Shooter.Core;

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
            //activeWeaponParticle.Play();
            myInput = GetComponent<PlayerInput>();
        }

        void Update()
        {
            var weaponEmission = activeWeaponParticle.emission;
            if (myInput.fire == true)
            {
                activeWeaponParticle.Play();
                //weaponEmission.enabled = true;
                //activeWeaponParticle.Play();

            }
            else
            {
                activeWeaponParticle.Stop();
                
                //weaponEmission.enabled = false;
                //activeWeaponParticle.Pause();

            }
        }


    }
}

