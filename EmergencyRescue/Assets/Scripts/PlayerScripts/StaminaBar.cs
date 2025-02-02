﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private float currentStamina;
    private const float maxStamina = 10f;

    //checks to see if currentStamina = maxStamina
    public bool equalized;
    public float regenAmount;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    public Coroutine regen;
    public static StaminaBar instance;

    void Awake() 
    {
        instance = this;
    }

    void Start() 
    {
        currentStamina = maxStamina;
        equalized = true;
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
        fill.color = gradient.Evaluate(1f);
    }

    void Update()
    {
        if(currentStamina != maxStamina)
        {
            currentStamina += regenAmount * Time.deltaTime;
            slider.value = currentStamina;
        }

        if(currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            equalized = true;
            slider.value = maxStamina;
        }
    }

    public void UseStamina()
    {
        if(currentStamina >= maxStamina)
        {
            currentStamina = 0f;
            equalized = false;
            slider.value = currentStamina;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
