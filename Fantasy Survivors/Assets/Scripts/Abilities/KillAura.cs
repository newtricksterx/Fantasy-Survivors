using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAura : Ability
{
    public float damageToDeal;
    public float tickRate;
    public float slow;

    protected CircleCollider2D circleCollider;

    Dictionary<int, Enemy> enemiesHit;

    public List<float> scaleBasedOnLevel;

    // Start is called before the first frame update
    protected override void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        enemiesHit = new Dictionary<int, Enemy>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        circleCollider.OverlapCollider(filter, hits);
        GetHits();

    }

    protected override void OnCollide(Collider2D coll)
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

            StartCoroutine(Tick(coll.gameObject.GetComponent<Enemy>()));
        }
    }

    IEnumerator Tick(Enemy enemy)
    {
        int enemyInstanceID = enemy.GetInstanceID();

        enemiesHit.Add(enemyInstanceID, enemy);

        yield return new WaitForSeconds(tickRate);

        enemiesHit.Remove(enemyInstanceID);
    }

    public override void LevelEffect()
    {
        damageToDeal = levelEffect[AbilitiesManager.instance.abilitiesToLevels[gameObject]];
        transform.localScale = new Vector3(scaleBasedOnLevel[AbilitiesManager.instance.abilitiesToLevels[gameObject]], scaleBasedOnLevel[AbilitiesManager.instance.abilitiesToLevels[gameObject]], 1);
    }
}
