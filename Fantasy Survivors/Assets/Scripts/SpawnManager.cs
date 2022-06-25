using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> listOfEnemyPrefabs = new List<GameObject>();

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    public float spawnCooldown;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnCooldown > spawnTime)
        {
            SpawnEnemy();
            spawnTime = Time.time;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        int randomIndex = Random.Range(0, listOfEnemyPrefabs.Count);

        Instantiate(listOfEnemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }

    Vector2 GetSpawnPosition()
    {
        float xSpawn = Random.Range(xMin, xMax);
        float ySpawn = Random.Range(yMin, yMax);

        return new Vector2(xSpawn, ySpawn);
    }
}
