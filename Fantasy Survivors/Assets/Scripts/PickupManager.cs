using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager instance;

    public GameObject pickupExperiencePrefab;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnExperience(Vector3 spawnPos)
    {
        Instantiate(pickupExperiencePrefab, spawnPos, Quaternion.identity);
    }
}
