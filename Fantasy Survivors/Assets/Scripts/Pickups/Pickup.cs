using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Collidable
{
    public float instanceDuration;

    public AudioClip playOnPickup;

    protected GameObject player;

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

    public void PlayPickupSound()
    {
        if(SoundManager.instance != null)
        {
            SoundManager.instance.PlaySoundClip(playOnPickup);
        }
    }
}
