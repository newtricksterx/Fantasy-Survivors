using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager instance;

    public int maxAbilityLevel = 5;

    public SpawnAbilities[] spawnAbilities;
    //public List<GameObject> abilityGameObjects;
    //public List<GameObject> spawnAbilitiesGameObjects;
    public Dictionary<GameObject, int> abilitiesToLevels;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        abilitiesToLevels = new Dictionary<GameObject, int>();

        spawnAbilities = FindObjectsOfType<SpawnAbilities>();

        /*
        foreach (SpawnAbilities s in spawnAbilities)
        {
            spawnAbilitiesGameObjects.Add(s.gameObject);
            abilityGameObjects.Add(s.abilityToSpawn);
        }
        */

        foreach (SpawnAbilities go in spawnAbilities) //abilityGameObjects)
        {
            abilitiesToLevels.Add(go.abilityToSpawn, 0);
            Ability ability = go.abilityToSpawn.GetComponent<Ability>();
            ability.LevelEffect();
            ShowLevels(go.abilityToSpawn);
        }

        Debug.Log(FindObjectsOfType<SpawnAbilities>().Length);
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
}
