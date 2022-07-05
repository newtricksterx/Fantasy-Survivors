using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingFireball : Ability
{
    public float damageToDeal;
    public float rotateSpeed;
    public float distance;

    protected CircleCollider2D circleCollider;

    private Transform player;

    private Dictionary<int, Enemy> enemiesHit;

    // Start is called before the first frame update
    protected override void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        player = GameObject.Find("Player").transform;
        enemiesHit = new Dictionary<int, Enemy>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        circleCollider.OverlapCollider(filter, hits);
        GetHits();
        Rotate();
    }

    private void Rotate()
    {
        transform.position = player.position + new Vector3(-Mathf.Cos(Time.time * rotateSpeed) * distance, Mathf.Sin(Time.time * rotateSpeed) * distance, 0);
    }
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy") && !enemiesHit.ContainsKey(coll.gameObject.GetComponent<Enemy>().GetInstanceID()))
        {
            Damage damage = new Damage
            {
                damageToDeal = damageToDeal
            };

            coll.gameObject.SendMessage("ReceiveDamage", damage);

            string msg = damageToDeal.ToString();

            GameManager.instance.ShowText(msg, 22, Color.white, coll.transform.position, Vector3.zero, 1f);
            enemiesHit.Add(coll.gameObject.GetComponent<Enemy>().GetInstanceID(), coll.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy") && enemiesHit.ContainsKey(coll.gameObject.GetComponent<Enemy>().GetInstanceID()))
        {
            enemiesHit.Remove(coll.gameObject.GetComponent<Enemy>().GetInstanceID());
        }
    }
}
