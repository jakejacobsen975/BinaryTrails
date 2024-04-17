using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{
    
    public Slider slider;
    public Gradient gradient;

    public Image fill;

    public void SetMaxHealth(int Health){
        slider.maxValue = Health;
        slider.value = Health;

        gradient.Evaluate(1f);

    }
    // Update is called once per frame
    public void SetHealth(int Health){
        slider.value = Health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }


}