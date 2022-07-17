using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAbilities : MonoBehaviour
{
    public GameObject abilityToSpawn;
    public bool enableAbility;

    public virtual void SpawnGameObject()
    {
        // spawn or instantiate gameobject
        if (enableAbility)
        {
            abilityToSpawn.SetActive(enableAbility);
        }
    }

    public void EnableAbility()
    {
        enableAbility = true;
        Image slot = AbilityListManager.instance.GetEmptySlot();
        slot.sprite = GetComponent<SpriteRenderer>().sprite;
        slot.gameObject.SetActive(true);
    }

    public void DisableAbility()
    {
        enableAbility = false;
    }
}
