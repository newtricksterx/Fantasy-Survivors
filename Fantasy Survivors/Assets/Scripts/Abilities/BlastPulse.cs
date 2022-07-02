using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPulse : Ability
{
    public GameObject abilityToSpawn;

    // Start is called before the first frame update
    public float pulseCooldown = 1f;
    private float afterPulseShotTime;

    protected void Awake()
    {
        afterPulseShotTime = -pulseCooldown;
    }

    protected override void Update()
    {
        SpawnGameObject();
    }

    public void SpawnGameObject()
    {
        if (GameManager.instance.player != null && Time.time - pulseCooldown >= afterPulseShotTime && GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && GameObject.FindObjectsOfType<ProjectilePulse>().Length == 0)
        {
            Instantiate(abilityToSpawn, GameManager.instance.player.transform.position, Quaternion.identity);
            afterPulseShotTime = Time.time;
        }
    }

    public override void LevelEffect()
    {
        Projectile proj = abilityToSpawn.GetComponent<Projectile>();
        proj.projectileSpeed = levelEffect[AbilitiesManager.instance.abilitiesToLevels[gameObject]];
    }
}
