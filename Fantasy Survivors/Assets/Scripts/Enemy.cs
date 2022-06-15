using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatant
{
    public float damageToDeal;

    private GameObject player;
    private Animator anim;
    private void Awake()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    protected void FixedUpdate()
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

}
