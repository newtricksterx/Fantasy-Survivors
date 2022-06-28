using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager instance;

    public int maxAbilityLevel = 5;

    // abilities
    public List<SpawnAbilities> spawnAbilities;
    public List<GameObject> abilityGameObjects;
    public List<GameObject> spawnAbilitiesGameObjects;
    public Dictionary<GameObject, int> abilitiesToLevels;

    // Start is called before the first frame update
    void Awake()
    {
        abilitiesToLevels = new Dictionary<GameObject, int>();

        instance = this;

        foreach (SpawnAbilities s in AbilitiesManager.instance.spawnAbilities)
        {
            spawnAbilitiesGameObjects.Add(s.gameObject);
        }

        foreach (GameObject go in abilityGameObjects)
        {
            abilitiesToLevels.Add(go, 0);
            Ability ability = go.GetComponent<Ability>();
            ability.LevelEffect();
            ShowLevels(go);
        }
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
}
