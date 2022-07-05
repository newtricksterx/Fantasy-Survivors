using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public string poolerName;
    public GameObject poolObject;
    public Queue<GameObject> pooledObjects;
    public int amountToPool;

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject instance = Instantiate(poolObject, transform.transform.position, Quaternion.identity);
            instance.SetActive(false);
            pooledObjects.Enqueue(instance);
        }
    }

    public GameObject GetPooledObject()
    {
        if(pooledObjects.Count > 0)
        {
            return pooledObjects.Dequeue();
        }

        GameObject newInstance = Instantiate(poolObject, transform.transform.position, Quaternion.identity);
        newInstance.SetActive(false);
        return newInstance;
    }

    public GameObject SpawnObject(Vector2 spawnPos)
    {
        GameObject inst = GetPooledObject();
        inst.SetActive(true);
        inst.transform.position = spawnPos;

        return inst;
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        pooledObjects.Enqueue(enemy);
    }
}
