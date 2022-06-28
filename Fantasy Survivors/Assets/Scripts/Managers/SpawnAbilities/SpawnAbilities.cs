using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void DisableAbility()
    {
        enableAbility = false;
    }
}
