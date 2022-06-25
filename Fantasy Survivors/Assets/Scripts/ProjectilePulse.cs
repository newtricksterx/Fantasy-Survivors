using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePulse : Projectile
{
    private Transform player;
    private float xScale;

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    private Enemy[] listOfEnemies;
    private Enemy randomEnemy;

    protected override void Awake()
    {
        base.Awake();

        //player = GameObject.Find("Player").transform;
        //xScale = player.localScale.x;
        //transform.localScale = new Vector3(xScale * transform.localScale.x, transform.localScale.y, 1);

        listOfEnemies = GameObject.FindObjectsOfType<Enemy>();
        randomEnemy = listOfEnemies[Random.Range(0, listOfEnemies.Length)];
    }

    protected override void Update()
    {
        base.Update();
        ToMove();
        DestroyOutOfBounds();
    }

    public override void ToMove()
    {
        if(randomEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        //transform.LookAt(randomEnemy.transform.position);

        Vector3 dir = randomEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = Vector2.MoveTowards(transform.position, randomEnemy.transform.position, projectileSpeed * Time.deltaTime);
        /*
        if((randomEnemy.transform.position - transform.position).x >= 0)
        {
            transform.right = transform.position - randomEnemy.transform.position;
        }
        else
        {
            transform.right = randomEnemy.transform.position - transform.position;
        }
        */

        //transform.right = randomEnemy.transform.position - transform.position;
        //transform.Translate(new Vector2(xScale, 0) * projectileSpeed * Time.deltaTime);
    }

    protected virtual void DestroyOutOfBounds()
    {
        if (transform.position.y > yMax || transform.position.y < yMin)
        {
            Destroy(gameObject);
        }

        if (transform.position.x > xMax || transform.position.x < xMin)
        {
            Destroy(gameObject);
        }
    }

}
