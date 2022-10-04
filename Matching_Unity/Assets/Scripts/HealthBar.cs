using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText;

    public void Start(){
        slider = this.GetComponent<Slider>();
        healthText = this.GetComponentInChildren<TextMeshProUGUI>();

    }

    public void SetHealth(int health){
        slider.value = health;
        healthText.text = "HP: " + health;
    }
}
