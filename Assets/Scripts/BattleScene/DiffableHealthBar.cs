using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffableHealthBar : MonoBehaviour
{
    public Slider topSlider;
    public Slider diffSlider;

    public float minValue = 0;
    public float maxValue = 1;
    public float currentValue;

    public float topSliderSpeed = 5;    //How fast the value of topSlider moves towards currentValue, in units per second
    public float diffSliderSpeed = 1;   //How fast the value of diffSlider moves towards currentValue, in units per second
    public float diffSliderDelay = 1;   //After taking damage, the diffSlider will wait this long before it catches up to the topSlider.

    private float prevValue = 0f;       //What currentValue was last frame.
    private float delayCountdown = 0f;

    void Update()
    {
        //Ensure that the min and max of the sliders matches our min and max
        topSlider.minValue = minValue;
        topSlider.maxValue = maxValue;

        diffSlider.minValue = minValue;
        diffSlider.maxValue = maxValue;

        //If currentValue was changed since last frame, reset the delay countdown
        if (currentValue != prevValue)
        {
            delayCountdown = diffSliderDelay;
            prevValue = currentValue;
        }

        //Move the top slider towards currentValue
        topSlider.value = Mathf.MoveTowards(topSlider.value, currentValue, topSliderSpeed * Time.deltaTime);

        //Move the diff slider towards currentValue, if the delay has passed.
        if (delayCountdown <= 0)
        {
            diffSlider.value = Mathf.MoveTowards(diffSlider.value, currentValue, diffSliderSpeed * Time.deltaTime);
        }
        else
        {
            delayCountdown -= Time.deltaTime;
        }
    }

    public void ChangeValueImmediately(float value)
    {
        //Immediately changes the health bar's value, without gradually changing any bars

        currentValue = value;
        prevValue = value;

        topSlider.value = value;
        diffSlider.value = value;
    }

}
