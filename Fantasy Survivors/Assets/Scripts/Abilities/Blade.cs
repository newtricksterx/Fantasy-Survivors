using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Ability
{
    public float damageToDeal;
    public float slashCooldown;
    public Slash slash;
    public AudioClip slashSoundEffect;

    private float slashTime;

    // Start is called before the first frame update
    protected override void Start()
    {
        slashTime = 0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Time.time - slashCooldown >= slashTime)
        {
            Slash();
            slashTime = Time.time;
        }
    }

    public override void LevelEffect()
    {
        damageToDeal = levelEffect[AbilitiesManager.instance.abilitiesToLevels[gameObject]];
    }

    void Slash()
    {
        SoundManager.instance.PlaySoundClip(slashSoundEffect);
        slash.SlashEffect(damageToDeal);
    }
}
