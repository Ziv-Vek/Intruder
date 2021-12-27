using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Core
{
    public class PlayerHealthHUDIndicator : MonoBehaviour
    {
        [SerializeField] Image shipModel;
        [SerializeField] Color modelFullHealthColor;
        [SerializeField] Color modelZeroHealthColor;

        Slider healthSlider;
        Color lerpedShipModelColor;

        

        private void Start()
        {
            healthSlider = GetComponent<Slider>();
            SetMaxHealthInIndicator();
        }

        private void Update() {
            SetCurrentHealthInIndicator();
            SetShipModelColorIndicator();
        }

        private void SetMaxHealthInIndicator()
        {
            healthSlider.maxValue = GameObject.FindWithTag("Player").GetComponent<Health>().GetMaxHealth;
        }
        
        private void SetCurrentHealthInIndicator()
        {
            healthSlider.value = GameObject.FindWithTag("Player").GetComponent<Health>().GetCurrentHealth;
        }

        void SetShipModelColorIndicator()
        {
            lerpedShipModelColor = Color.Lerp(modelZeroHealthColor, modelFullHealthColor, healthSlider.normalizedValue);
            shipModel.color = lerpedShipModelColor;
        }
    }
}


