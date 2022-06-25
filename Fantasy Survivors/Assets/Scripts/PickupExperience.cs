using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupExperience : Pickup
{
    public int experienceOnPickup;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GrantXP(experienceOnPickup);
            Destroy(gameObject);
        }
    }
}
