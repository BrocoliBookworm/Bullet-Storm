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
        if(Mathf.Abs(currentStamina - maxStamina) <= Mathf.Epsilon)
        {
            if(currentStamina == maxStamina)
            {
                Debug.Log("stamina's equal");
            }
            Debug.Log("If passed");
            currentStamina = 0;
            Debug.Log("current stamina = 0");
            slider.value = currentStamina;
            Debug.Log("Slider at 0");
            fill.color = gradient.Evaluate(slider.normalizedValue);

            regen = StartCoroutine(RegenStamina());
            Debug.Log("Couroutine started");
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            Debug.Log("while started");
            currentStamina += maxStamina / 100;
            Debug.Log("increment");
            slider.value = currentStamina;
            Debug.Log("slideruptick");
            yield return regenTick;
        }
        
        regen = null;
    }
}
