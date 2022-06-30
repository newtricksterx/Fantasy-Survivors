using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lootbox : MonoBehaviour
{
    public static Lootbox instance;

    private Animator anim;

    public Image imageOfAbility;
    public bool playAnim;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        anim.SetBool("OpenChest", playAnim);

        // randomly select an ability to level up

        int abilityIndex = Random.Range(0, AbilitiesManager.instance.spawnAbilities.Length);

        //imageOfAbility.sprite = AbilitiesManager.instance.GetSpawnGameObject(abilityIndex).gameObject.GetComponent<SpriteRenderer>().sprite;

        StartCoroutine(DisplayAbility(abilityIndex));

        GameObject abilityGameObjectSelected = AbilitiesManager.instance.GetAbilityGameObject(abilityIndex);

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
    }

    IEnumerator DisplayAbility(int abilityIndex)
    {
        yield return new WaitForSecondsRealtime(2f);

        imageOfAbility.sprite = AbilitiesManager.instance.GetSpawnGameObject(abilityIndex).gameObject.GetComponent<SpriteRenderer>().sprite;
        imageOfAbility.color = new Color(imageOfAbility.color.r, imageOfAbility.color.g, imageOfAbility.color.b, 255f);
    }
}
