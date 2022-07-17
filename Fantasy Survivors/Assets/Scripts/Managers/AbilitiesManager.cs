using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager instance;

    public int maxAbilityLevel = 5;
    public int maxNumberOfAbilities = 5;

    public SpawnAbilities[] spawnAbilities;
    public Dictionary<GameObject, int> abilitiesToLevels;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        abilitiesToLevels = new Dictionary<GameObject, int>();

        spawnAbilities = FindObjectsOfType<SpawnAbilities>();

        foreach (SpawnAbilities go in spawnAbilities) //abilityGameObjects)
        {
            abilitiesToLevels.Add(go.abilityToSpawn, 0);
            Ability ability = go.abilityToSpawn.GetComponent<Ability>();
            ability.LevelEffect();
            ShowLevels(go.abilityToSpawn);
        }

        //Debug.Log(FindObjectsOfType<SpawnAbilities>().Length);
    }

    private void Update()
    {
        foreach(SpawnAbilities spawn in spawnAbilities)
        {
            spawn.SpawnGameObject();
        }
    }

    public void ShowLevels(GameObject ability)
    {
        Debug.Log( ability.name + ", " + abilitiesToLevels[ability].ToString());
    }

    public GameObject GetAbilityGameObject(int index)
    {
        return spawnAbilities[index].abilityToSpawn;
    }

    public GameObject GetSpawnGameObject(int index)
    {
        return spawnAbilities[index].gameObject;
    }

    public SpawnAbilities GetSpawnScript(int index)
    {
        return spawnAbilities[index];
    }

    public int GetNumberOfEnabledAbilities()
    {
        int num = 0;

        foreach(SpawnAbilities s in spawnAbilities)
        {
            if (s.enableAbility)
            {
                num++;
            }
        }

        return num;
    }

    public int GetNumOfMaxLevelAbilities()
    {
        int num = 0;


        foreach(GameObject go in abilitiesToLevels.Keys)
        {
            if (abilitiesToLevels[go] >= maxAbilityLevel)
            {
                num++;
            } 
        }

        return num;
    }
}
