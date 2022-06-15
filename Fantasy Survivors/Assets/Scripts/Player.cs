using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Combatant
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDelta = new Vector3(xInput, yInput);

        MoveMotor((new Vector3(xInput, yInput, 0)).normalized);

        if (moveDelta == Vector3.zero)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = coll.GetComponent<Enemy>();
            hp -= enemy.damageToDeal;
        }
    }

    protected override void PlayDeathAnim()
    {
        anim.SetBool("death", true);
    }
}
