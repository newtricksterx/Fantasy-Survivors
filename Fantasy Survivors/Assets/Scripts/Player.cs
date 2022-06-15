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
    void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDelta = new Vector3(xInput * movementSpeed, yInput * movementSpeed);

        MoveMotor(new Vector3(xInput, yInput, 0));

        if (moveDelta == Vector3.zero)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }

    }

    protected override void PlayDeathAnim()
    {
        Debug.Log("Death");
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

    }
}
