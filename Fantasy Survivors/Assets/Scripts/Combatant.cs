using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MovingObject
{
    public float maxHP;
    [SerializeField] float hp;

    private void Awake()
    {
        hp = maxHP;
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

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }
}
