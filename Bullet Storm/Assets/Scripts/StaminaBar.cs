using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private float currentStamina;
    private float maxStamina = 10;

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
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
        fill.color = gradient.Evaluate(1f);
    }

    public void UseStamina()
    {
        if(currentStamina == maxStamina)
        {
            currentStamina = 0;
            slider.value = currentStamina;
            fill.color = gradient.Evaluate(slider.normalizedValue);

            regen = StartCoroutine(RegenStamina());
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            slider.value = currentStamina;
            yield return regenTick;
        }
        
        regen = null;
    }
}
