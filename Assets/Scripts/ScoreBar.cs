using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fillImage;

    public void SetScoreThreshhold(int threshhold)
    {
        slider.maxValue = threshhold;
        slider.value = 0;

    }

    public void AddScore()
    {
        slider.value += 1;
        fillImage.color = gradient.Evaluate(slider.normalizedValue);

        if (slider.value >= slider.maxValue)
        {
            Debug.Log("score bar reach to the max!!");

            // TODO: - ganjiaqi. check if we need to do sth here!
        }
    }
}
