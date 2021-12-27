using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Shooter.Combat
{
    public class WeaponNameIndicator : MonoBehaviour
    {
        [SerializeField] string[] weaponName;

        public void SetWeaponText(int weaponNumber)
        {
            GetComponent<TextMeshProUGUI>().text = weaponName[weaponNumber];
        }
    }
}

