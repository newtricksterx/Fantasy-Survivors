using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorManager : MonoBehaviour
{
    public Image imageOfAbility;

    public int abilityIndex;

    public Text selectText;

    //public List<SpawnAbilities> spawnAbilities;
    //List<GameObject> abilitiesGameObjects;

    public void GetAbility()
    {
        if (AbilitiesManager.instance.maxAbilityLevel <= AbilitiesManager.instance.abilitiesToLevels[AbilitiesManager.instance.abilityGameObjects[abilityIndex]])
        {
            selectText.text = "MAX";
        }
        else
        {
            selectText.text = "SELECT";
        }

        //Debug.Log(AbilitiesManager.instance.spawnAbilitiesGameObjects.Count);

        abilityIndex = Random.Range(0, AbilitiesManager.instance.spawnAbilitiesGameObjects.Count);

        while (GameManager.instance.abilitiesPicked.Contains(AbilitiesManager.instance.abilityGameObjects[abilityIndex]))
        {
            abilityIndex = Random.Range(0, AbilitiesManager.instance.spawnAbilitiesGameObjects.Count);
        }

        imageOfAbility.sprite = AbilitiesManager.instance.spawnAbilitiesGameObjects[abilityIndex].GetComponent<SpriteRenderer>().sprite;

        GameManager.instance.abilitiesPicked.Add(AbilitiesManager.instance.abilityGameObjects[abilityIndex]);
    }

    public void Select()
    {
        if(AbilitiesManager.instance.maxAbilityLevel <= AbilitiesManager.instance.abilitiesToLevels[AbilitiesManager.instance.abilityGameObjects[abilityIndex]]){
            GameManager.instance.OnSelection();
            return;
        }

        if (AbilitiesManager.instance.spawnAbilities[abilityIndex].enableAbility)
        {
            Ability ability = AbilitiesManager.instance.abilityGameObjects[abilityIndex].GetComponent<Ability>();

            // level up the active ability
            AbilitiesManager.instance.abilitiesToLevels[AbilitiesManager.instance.abilityGameObjects[abilityIndex]] += 1;
            ability.LevelEffect();
            
            AbilitiesManager.instance.ShowLevels(AbilitiesManager.instance.abilityGameObjects[abilityIndex]);
        }
        else
        {
            AbilitiesManager.instance.spawnAbilities[abilityIndex].EnableAbility();
        }

        GameManager.instance.OnSelection();
    }
}
