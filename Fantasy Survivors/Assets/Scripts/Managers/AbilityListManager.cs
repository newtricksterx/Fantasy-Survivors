using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityListManager : MonoBehaviour
{
    public static AbilityListManager instance;

    public List<Image> imageSlots;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    public Image GetEmptySlot()
    {
        foreach(Image slot in imageSlots)
        {
            if(!slot.gameObject.activeInHierarchy)
            {
                return slot;
            }
        }

        return null;
    }
}
