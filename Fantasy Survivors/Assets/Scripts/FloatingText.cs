using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool isActive;
    public GameObject textGameObject;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        isActive = true;
        lastShown = Time.time;
        textGameObject.SetActive(isActive);
    }

    public void Hide()
    {
        isActive = false;
        textGameObject.SetActive(isActive);
    }

    public void UpdateFloatingText()
    {
        if (!isActive)
        {
            return;
        }

        if(Time.time - lastShown > duration)
        {
            Hide();
        }

        textGameObject.transform.position += motion * Time.deltaTime;
    }
}
