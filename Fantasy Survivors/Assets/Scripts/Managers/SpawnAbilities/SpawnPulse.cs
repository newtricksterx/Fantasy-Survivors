using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPulse : SpawnAbilities
{
    public float pulseCooldown = 1f;
    private float afterPulseShotTime;

    protected void Start()
    {
        afterPulseShotTime = -pulseCooldown;
    }

    public override void SpawnGameObject()
    {
        //Debug.Log(Time.time - pulseCooldown >= afterPulseShotTime);
        //Debug.Log((Time.time - pulseCooldown).ToString() + ", " + afterPulseShotTime.ToString()); ;

        // CHILL

        if (GameManager.instance.player != null && enableAbility && Time.time - pulseCooldown >= afterPulseShotTime && GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && GameObject.FindObjectsOfType<ProjectilePulse>().Length == 0)
        {
            Instantiate(abilityToSpawn, GameManager.instance.player.transform.position, Quaternion.identity);
            afterPulseShotTime = Time.time;
        }
    }
}
