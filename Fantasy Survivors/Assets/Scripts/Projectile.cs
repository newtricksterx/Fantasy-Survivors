using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Collidable
{
    public float projectileSpeed;
    public float damageToDealMin;
    public float damageToDealMax;

    protected float damageToDeal;

    protected virtual void Awake()
    {
        CalculateDamageToDeal();
    }

    public virtual void ToMove()
    {
        // implement how the projectile will move
    }

    protected virtual void CalculateDamageToDeal()
    {
        damageToDeal = Random.Range(damageToDealMin, damageToDealMax);
        damageToDeal = Mathf.RoundToInt(damageToDeal);
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            Damage damage = new Damage
            {
                damageToDeal = damageToDeal
            };

            coll.SendMessage("ReceiveDamage", damage);

            string msg = damageToDeal.ToString(); 

            GameManager.instance.ShowText(msg, 20, Color.white, coll.transform.position, Vector3.zero, 1f);

            Destroy(gameObject);
        }
    }
}
