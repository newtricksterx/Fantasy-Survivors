using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Collidable
{
    public float instanceDuration;

    private GameObject player;
    private float timeToDespawn;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        timeToDespawn = Time.time + instanceDuration;
    }

    protected override void Update()
    {
        if(Time.time >= timeToDespawn)
        {
            Destroy(gameObject);
        }

        base.Update();
    }
}
