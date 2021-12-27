using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Combat
{
    public class CrosshairHandler : MonoBehaviour
    {
        [SerializeField] Sprite[] crosshairImages;

        public void SetCrosshair(int weaponNumber)
        {
            GetComponent<Image>().sprite = crosshairImages[weaponNumber];
        }
    }
}