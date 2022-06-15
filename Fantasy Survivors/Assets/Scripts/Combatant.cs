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

    protected virtual void Update()
    {
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

    protected abstract void PlayDeathAnim();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider: " + gameObject.name);
    }
}
