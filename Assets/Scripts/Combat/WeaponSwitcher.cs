using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.Controls;
using System;

namespace Shooter.Combat
{

    public class WeaponSwitcher : MonoBehaviour
    {
        PlayerInput playerInput;
        Weapon activeWeapon;
        [SerializeField] Weapon[] weapons;
        CrosshairHandler crosshairHandler;
        WeaponImageIndicator weaponImageIndicator;
        WeaponNameIndicator weaponNameIndicator;

        private void Awake() {
            playerInput = GetComponentInParent<PlayerInput>();
            weapons = GetComponentsInChildren<Weapon>(true);
            crosshairHandler = FindObjectOfType<CrosshairHandler>();
            weaponImageIndicator = FindObjectOfType<WeaponImageIndicator>();
            weaponNameIndicator = FindObjectOfType<WeaponNameIndicator>();
        }

        private void Start() {
            CheckActiveWeaponType();
        }
        
        private void Update() 
        {
            ScrollWeapons();
        }

        void CheckActiveWeaponType()
        {
            for (int i = 0 ; i < weapons.Length ; i++)
            {
                if (weapons[i].gameObject.activeSelf)
                {
                    activeWeapon = weapons[i];
                    SetHUDWeaponDisplay(i);
                    return;
                }
            }
            
            Debug.Log("No active weapon was found!");
        }


        private void ScrollWeapons()
        {
            if (Mathf.Abs(playerInput.weaponScroll) <= Mathf.Epsilon) { return; }

            if (playerInput.weaponScroll > 0)
            {
                for (int i = 0 ; i < weapons.Length ; i++)
                {
                    if (weapons[i].GetWeaponType == activeWeapon.GetWeaponType)
                    {
                        weapons[i].gameObject.SetActive(false);
                        
                        if (i + 1 == weapons.Length)
                        {
                            ActivateNewWeapon(0);
                            break;
                        }
                        else
                        {
                            ActivateNewWeapon(i + 1);
                            break;
                        }
                    } 
                }
            }

            if (playerInput.weaponScroll < 0)
            {
                for (int i = weapons.Length - 1 ; i >= 0 ; i--)
                {
                    if (weapons[i].GetWeaponType == activeWeapon.GetWeaponType)
                    {
                        weapons[i].gameObject.SetActive(false);
                        
                        if (i - 1 == -1)
                        {
                            ActivateNewWeapon(weapons.Length - 1);
                            break;
                        }
                        else
                        {
                            ActivateNewWeapon(i - 1);
                            break;
                        }
                    }
                }
            }
        }

        private void ActivateNewWeapon(int weaponIncrement)
        {
            weapons[weaponIncrement].gameObject.SetActive(true);
            activeWeapon = weapons[weaponIncrement];
            GetComponentInParent<PlayerAttacker>().SetActiveWeapon(weapons[weaponIncrement].GetComponent<ParticleSystem>());
            SetHUDWeaponDisplay(weaponIncrement);
        }

        private void SetHUDWeaponDisplay(int weaponIncrement)
        {
            crosshairHandler.SetCrosshair(weaponIncrement);
            weaponImageIndicator.SetHUDWeaponImage(weaponIncrement);
            weaponNameIndicator.SetWeaponText(weaponIncrement);
        }
    }
}


