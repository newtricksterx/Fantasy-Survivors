using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    // enemies to spawn
    public List<GameObject> listOfEnemyPrefabs = new List<GameObject>();

    public GameObject undeadBoss;

    public float timeSpawnBoss;

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    public float spawnCooldown;
    private float spawnTime;

    // Pickups to randomly spawn
    public GameObject pickupHealthpack;
    public int maxHealthpacks;

    // spawn healthpacks
    public float healhpackCooldown;
    private float lastSpawnedHealthpack;

    // time to spawn boss
    public float bossCooldown;
    private float lastSpawnedBoss;

    // object enemy pooler
    public GameObject enemyPoolerObject;
    private void Awake()
    {
        instance = this;
        lastSpawnedHealthpack = 0f;
        lastSpawnedBoss = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.RoundToInt(Time.timeSinceLevelLoad) > 0 && Mathf.RoundToInt(Time.timeSinceLevelLoad) % timeSpawnBoss == 0 && Time.timeSinceLevelLoad - bossCooldown >= lastSpawnedBoss)
        {
            SpawnUndeadBoss();
            lastSpawnedBoss = Time.timeSinceLevelLoad;
        }

        if(Time.timeSinceLevelLoad - spawnCooldown >= spawnTime)
        {
            SpawnEnemy();
            spawnTime = Time.timeSinceLevelLoad;
        }

        if(Time.timeSinceLevelLoad - lastSpawnedHealthpack >= healhpackCooldown && FindObjectsOfType<PickupHealthPack>().Length < maxHealthpacks)
        {
            SpawnHealthPack();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        ObjectPooler[] enemyPoolers = enemyPoolerObject.GetComponents<ObjectPooler>();

        int numOfEnemies = enemyPoolers.Length;

        int randomIndex = Random.Range(0, numOfEnemies);

        ObjectPooler enemyPoolerPicked = enemyPoolers[randomIndex];

        GameObject enemyInstance = enemyPoolerPicked.SpawnObject(spawnPosition);

        //int randomIndex = Random.Range(0, listOfEnemyPrefabs.Count);

        //GameObject enemyInstance = Instantiate(listOfEnemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        if(Mathf.RoundToInt(Time.timeSinceLevelLoad / 60) > 0)
        {
            Enemy enemy = enemyInstance.GetComponent<Enemy>();

            int multiplier = Mathf.RoundToInt(Time.timeSinceLevelLoad / 60);

            enemy.SetInitialHealth(enemy.maxHP * multiplier);
        }
        
    }

    void SpawnUndeadBoss()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        Instantiate(undeadBoss, spawnPosition, Quaternion.identity);
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
