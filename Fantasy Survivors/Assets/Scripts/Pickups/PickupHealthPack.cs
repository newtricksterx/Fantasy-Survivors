using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealthPack : Pickup
{
    public float hpToAdd;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            PlayPickupSound();
            player.GetComponent<Player>().GrantHP(hpToAdd);

            string msg = "+ " + hpToAdd.ToString();

            GameManager.instance.ShowText(msg, 20, Color.green, player.transform.position, Vector3.zero, 1);
            Destroy(gameObject);
        }
    }
}
