using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Controls;
using Shooter.Core;
using System;
using UnityEngine.UI;

namespace Shooter.Combat
{
    public class PlayerAttacker : MonoBehaviour
    {
        // Config
        [SerializeField] ParticleSystem activeWeaponParticle;
        [SerializeField] [Range(0 , 1)] float shootingSFXVolume = 0.5f;
        
        // Cached references
        [SerializeField] CanvasRenderer crosshair;
        
        PlayerInput myInput;


        void Start()
        {
            myInput = GetComponent<PlayerInput>();
            activeWeaponParticle.Play();
        }

        void Update()
        {
            Fire(myInput.fire);
            MarkCrosshair();
        }

        private void Fire(bool enableShooting)
        {
            var shootAudio = activeWeaponParticle.GetComponent<Weapon>().ShootingAudio;
            int currentNumberOfAliveParticles = 0;
            if (enableShooting)
            {
                currentNumberOfAliveParticles = CountCurrentParticles();
                Debug.Log("current number of particles is: " + currentNumberOfAliveParticles);
            }

            var weaponEmission = activeWeaponParticle.emission;
            weaponEmission.enabled = enableShooting;
            // while (enableShooting)
            // {
            //     GetComponent<AudioSource>().PlayOneShot(shootAudio, shootingSFXVolume);
            //     playShootingSFX(currentNumberOfAliveParticles);
            // }
            
            if (enableShooting)
            {
                //var shootAudio = activeWeaponParticle.GetComponent<Weapon>().ShootingAudio;
                GetComponent<AudioSource>().PlayOneShot(shootAudio, shootingSFXVolume);
                playShootingSFX(currentNumberOfAliveParticles);
            }
        }

        private int CountCurrentParticles()
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[activeWeaponParticle.particleCount];
            return activeWeaponParticle.GetParticles(particles);
        }

        private void playShootingSFX(int currentNumberOfAliveParticles)
        {
            var shootAudio = activeWeaponParticle.GetComponent<Weapon>().ShootingAudio;
            
            var newAliveParticlesCount = CountCurrentParticles();
            if (currentNumberOfAliveParticles != newAliveParticlesCount)
            {
                GetComponent<AudioSource>().PlayOneShot(shootAudio, shootingSFXVolume);
            }
        }

        void MarkCrosshair()
        {
            string detectedObjecTag = null;
            float weaponRange = activeWeaponParticle.GetComponent<Weapon>().Range;
            
            var ray = new Ray(transform.position, transform.forward);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, weaponRange))
            {
                detectedObjecTag = hit.transform.gameObject.tag;

                if (detectedObjecTag == "Enemy")
                {
                    ChangeCrosshairColor(Color.red);
                }
                else
                {
                    ChangeCrosshairColor(Color.white);
                }
            }
            else
            {
                ChangeCrosshairColor(Color.white);
            }

        }

        private void ChangeCrosshairColor(Color32 color)
        {
            crosshair.GetComponent<Image>().color = color;
        }
    }
}

