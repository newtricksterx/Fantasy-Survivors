using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Player player;

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x - 0.01f, player.transform.position.y - 0.4f, player.transform.position.z);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        //slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
