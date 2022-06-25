using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatant
{
    public float damageToDeal;
    public float attackCooldown = 0.2f;

    private float lastAttack = -0.2f;
    private GameObject player;
    private Animator anim;
    private void Awake()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist == 0)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }

        MoveMotor((player.transform.position - transform.position).normalized);
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Damage damage = new Damage
            {
                damageToDeal = damageToDeal
            };

            if(Time.time - lastAttack > attackCooldown)
            {
                coll.SendMessage("ReceiveDamage", damage);
                lastAttack = Time.time;

                HurtEffect player = coll.gameObject.GetComponent<HurtEffect>();
                player.Flash();
            }
        }
    }

}
