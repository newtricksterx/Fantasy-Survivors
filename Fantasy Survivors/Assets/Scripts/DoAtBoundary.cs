using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoAtBoundary : Collidable
{
    public Transform teleportTo;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Enemy"))
        {
            coll.transform.position = teleportTo.position;
        }
    }
}
