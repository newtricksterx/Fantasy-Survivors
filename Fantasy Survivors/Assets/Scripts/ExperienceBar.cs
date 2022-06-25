using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
    public Text lvlText;

    public void SetMaxExp(float maxExp)
    {
        slider.maxValue = maxExp;
    }

    public void ResetExpBar()
    {
        slider.value = 0;
    }

    public void SetExp(float exp)
    {
        slider.value = exp;
    }

    public void SetLevelText(int level)
    {
        lvlText.text = "LV " + level.ToString();
    }
}
