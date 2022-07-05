using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MovingObject
{
    public float maxHP;
    public AudioClip hitSound;
    [SerializeField] protected float hp;
    protected HurtEffect hurtEffect;

    protected override void Start()
    {
        base.Start();
        hp = maxHP;
        hurtEffect = GetComponent<HurtEffect>();
    }

    protected override void Update()
    {
        base.Update();

        if(hp <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        hp = 0;
        PlayDeathAnim();
    }

    protected virtual void PlayDeathAnim()
    {
        //Debug.Log("Death");
    }

    protected virtual void ReceiveDamage(Damage damage)
    {
        hp -= damage.damageToDeal;
        SoundManager.instance.PlaySoundClip(hitSound);
    }
}
