using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Combatant
{
    private Animator anim;
    public HealthBar healthBar;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHP);
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if(hp > 0)
        {
            //Debug.Log("Can Move");
            Move();
        }
    }

    protected void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        //Debug.Log(xInput);

        Vector3 moveDelta = new Vector3(xInput, yInput);

        MoveMotor((new Vector3(xInput, yInput, 0)).normalized);

        if (moveDelta == Vector3.zero)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
            //Debug.Log("Player is Moving");
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = coll.GetComponent<Enemy>();
        }
    }

    protected override void Death()
    {
        base.Death();
        gameObject.SetActive(false);
    }

    protected override void PlayDeathAnim()
    {
        anim.SetBool("death", true);
    }
    protected override void ReceiveDamage(Damage damage)
    {
        base.ReceiveDamage(damage);
        healthBar.SetHealth(hp);
    }

}
