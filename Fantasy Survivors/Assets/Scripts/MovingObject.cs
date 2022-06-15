using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : Collidable
{
    public float movementSpeed;
    public float scale;

    protected RaycastHit2D hit;

    protected void MoveMotor(Vector3 input)
    {
        Vector3 moveDelta = new Vector3(input.x * movementSpeed, input.y * movementSpeed);

        if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-scale, scale, scale);
        }
        else if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
