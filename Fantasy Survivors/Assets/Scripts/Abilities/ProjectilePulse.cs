using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePulse : Projectile
{
    private Transform player;
    private float xScale;

    private Enemy[] listOfEnemies;
    private Enemy randomEnemy;

    protected override void Awake()
    {
        base.Awake();

        listOfEnemies = GameObject.FindObjectsOfType<Enemy>();
        randomEnemy = listOfEnemies[Random.Range(0, listOfEnemies.Length)];
    }

    protected override void Update()
    {
        base.Update();
        ToMove();
    }

    public override void ToMove()
    {
        if(randomEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = randomEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = Vector2.MoveTowards(transform.position, randomEnemy.transform.position, projectileSpeed * Time.deltaTime);
    }
}
