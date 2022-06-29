using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorManager : MonoBehaviour
{
    public Image imageOfAbility;

    public int abilityIndex;

    public Text selectText;
    public Text abilityName;

    public void GetAbility()
    {
        if (AbilitiesManager.instance.maxAbilityLevel <= AbilitiesManager.instance.abilitiesToLevels[AbilitiesManager.instance.GetAbilityGameObject(abilityIndex)])
        {
            selectText.text = "MAX";
        }
        else
        {
            selectText.text = "SELECT";
        }

        //Debug.Log(AbilitiesManager.instance.spawnAbilitiesGameObjects.Count);

        abilityIndex = Random.Range(0, AbilitiesManager.instance.spawnAbilities.Length);

        while (GameManager.instance.abilitiesPicked.Contains(AbilitiesManager.instance.GetAbilityGameObject(abilityIndex)))
        {
            abilityIndex = Random.Range(0, AbilitiesManager.instance.spawnAbilities.Length);
        }

        imageOfAbility.sprite = AbilitiesManager.instance.GetSpawnGameObject(abilityIndex).gameObject.GetComponent<SpriteRenderer>().sprite;

        GameManager.instance.abilitiesPicked.Add(AbilitiesManager.instance.GetAbilityGameObject(abilityIndex));

        abilityName.text = AbilitiesManager.instance.GetAbilityGameObject(abilityIndex).GetComponent<Ability>().abilityName;
    }

    public void Select()
    {
        GameObject abilityGameObjectSelected = AbilitiesManager.instance.GetAbilityGameObject(abilityIndex);

        if (AbilitiesManager.instance.maxAbilityLevel <= AbilitiesManager.instance.abilitiesToLevels[abilityGameObjectSelected]){
            GameManager.instance.OnSelection();
            return;
        }

        if (AbilitiesManager.instance.spawnAbilities[abilityIndex].enableAbility)
        {
            Ability ability = AbilitiesManager.instance.GetAbilityGameObject(abilityIndex).GetComponent<Ability>();

            // level up the active ability
            AbilitiesManager.instance.abilitiesToLevels[abilityGameObjectSelected] += 1;
            ability.LevelEffect();
            
            AbilitiesManager.instance.ShowLevels(abilityGameObjectSelected);
        }
        else
        {
            AbilitiesManager.instance.spawnAbilities[abilityIndex].EnableAbility();
        }

        GameManager.instance.OnSelection();
    }
}
