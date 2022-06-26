using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // enemies to spawn
    public List<GameObject> listOfEnemyPrefabs = new List<GameObject>();

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    public float spawnCooldown;
    private float spawnTime;

    // Pickups to randomly spawn
    public GameObject pickupHealthpack;
    public int maxHealthpacks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnCooldown >= spawnTime)
        {
            SpawnEnemy();
            spawnTime = Time.time;
        }

        if(GameObject.FindObjectsOfType<PickupHealthPack>().Length < maxHealthpacks)
        {
            SpawnHealthPack();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        int randomIndex = Random.Range(0, listOfEnemyPrefabs.Count);

        Instantiate(listOfEnemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }

    void SpawnHealthPack()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        Instantiate(pickupHealthpack, spawnPosition, Quaternion.identity);
    }

    Vector2 GetSpawnPosition()
    {
        float xSpawn = Random.Range(xMin, xMax);
        float ySpawn = Random.Range(yMin, yMax);

        return new Vector2(xSpawn, ySpawn);
    }


}
