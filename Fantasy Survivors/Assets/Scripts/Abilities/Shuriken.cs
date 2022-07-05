using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Projectile
{
    public List<float> levelEffect2;

    public float xInput;
    public float yInput;

    private void GetInputs()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            xInput = GameManager.instance.player.transform.localScale.x;
            yInput = 0;
        }
        else
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
        }
    }

    protected override void Awake()
    {
        base.Awake();

        GetInputs();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ToMove();
    }

    public override void ToMove()
    {
        // move player
        transform.position += new Vector3(xInput, yInput) * Time.deltaTime * projectileSpeed;
    }
}
