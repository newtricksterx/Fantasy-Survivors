using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatant
{
    public string enemyName;

    public float damageToDeal;
    public float attackCooldown = 0.2f;

    protected float lastAttack;
    protected GameObject player;
    protected Animator anim;
    protected virtual void Awake()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        lastAttack = -attackCooldown;
    }

    protected virtual void FixedUpdate()
    {
        if(player != null)
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist == 0 || movementSpeed == 0)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }

        MoveMotor((player.transform.position - transform.position).normalized);
    }

    protected override void Death()
    {
        GameManager.instance.AddToKillCount();
        GameManager.instance.SetKillCount();
        Drop();
        base.Death();

        this.gameObject.SetActive(false);
        GameManager.instance.DespawnEnemyObject(this);
        hp = maxHP;
        //Destroy(gameObject);
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {

            float reduceDamagebyBy = 0f;
            Armor armor = FindObjectOfType<Armor>();

            if (armor.gameObject.activeInHierarchy)
            {
                reduceDamagebyBy = armor.armorValue;
            }

            Damage damage;

            if (damageToDeal - reduceDamagebyBy < 0)
            {
                damage = new Damage
                {
                    damageToDeal = 0
                };
            }
            else
            {
                damage = new Damage
                {
                    damageToDeal = damageToDeal - reduceDamagebyBy
                };
            }

            if(Time.time - lastAttack > attackCooldown)
            {
                coll.SendMessage("ReceiveDamage", damage);
                lastAttack = Time.time;

                HurtEffect player = coll.gameObject.GetComponent<HurtEffect>();
                player.Flash();
            }
        }
    }

    protected virtual void Drop()
    {
        PickupManager.instance.SpawnExperience(transform.position);
    }

    public void SetInitialHealth(float init_hp)
    {
        maxHP = init_hp;
        hp = init_hp;
    }
}
