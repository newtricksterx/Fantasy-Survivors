using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenEngine : Ability
{
    public GameObject abilityToSpawn;

    public AudioClip shurikenSoundEffect;

    public List<float> levelEffect2;

    // Start is called before the first frame update
    public float cooldown = 1f;
    private float afterShotTime;

    protected void Awake()
    {
        afterShotTime = -cooldown;
    }

    protected override void Update()
    {
        SpawnGameObject();
    }

    public void SpawnGameObject()
    {
        if (GameManager.instance.player != null && Time.time - cooldown >= afterShotTime)
        {
            SoundManager.instance.PlaySoundClip(shurikenSoundEffect);
            Instantiate(abilityToSpawn, GameManager.instance.player.transform.position, Quaternion.identity);
            afterShotTime = Time.time;
        }
    }

    public override void LevelEffect()
    {
        Projectile proj = abilityToSpawn.GetComponent<Projectile>();
        proj.damageToDealMin = levelEffect[AbilitiesManager.instance.abilitiesToLevels[gameObject]];
        proj.damageToDealMax = levelEffect2[AbilitiesManager.instance.abilitiesToLevels[gameObject]];
    }
}
