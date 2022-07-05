using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootboxCollidable : Pickup
{
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            SoundManager.instance.PlaySoundClip(playOnPickup);
            GameManager.instance.lootboxPanel.transform.position = GameObject.Find("Player").transform.position;
            GameManager.instance.lootboxPanel.SetActive(true);

            Lootbox.instance.playAnim = true;
            Lootbox.instance.OpenChest();
            Lootbox.instance.playAnim = false;

            Destroy(gameObject);
        }
    }
}
