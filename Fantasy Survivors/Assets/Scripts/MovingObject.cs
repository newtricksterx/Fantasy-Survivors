using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float movementSpeed;
    public float scale;

    protected void MoveMotor(Vector3 input)
    {
        Vector3 moveDelta = new Vector3(input.x * movementSpeed, input.y * movementSpeed);

        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(-scale, scale, scale);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }

        transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
    }
}
