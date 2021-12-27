using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Combat
{
    public class WeaponImageIndicator : MonoBehaviour
    {
        [SerializeField] Sprite[] weaponImage;

        public void SetHUDWeaponImage(int weaponNumber)
        {
            GetComponent<Image>().sprite = weaponImage[weaponNumber];
        }
    }
}
