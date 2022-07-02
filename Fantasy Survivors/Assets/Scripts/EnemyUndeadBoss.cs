using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUndeadBoss : Enemy
{
    public float attackRange;
    public float slashDamage;

    public float xOffset;
    public float yOffset;

    public float slashCooldown;
    public bool isAttacking = false;
    protected float lastSlash;

    protected override void Awake()
    {
        base.Awake();
        lastSlash = -slashCooldown;
    }

    protected override void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (player != null && !isAttacking)
        {
            Move();
        }

        Attack();
    }

    protected override void PlayDeathAnim()
    {
        anim.SetBool("isDead", true);
        StartCoroutine(DestroyGameObject());
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    protected void Attack()
    {
        Vector3 circle = transform.position + new Vector3(xOffset, yOffset) * transform.localScale.x / 2;
        Collider2D[] inRange = Physics2D.OverlapCircleAll(circle, attackRange);

        foreach(Collider2D coll in inRange)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                if(Time.time - slashCooldown >= lastSlash)
                {
                    Debug.Log("hit player");

                    // simulate an enemy attack
                    StartCoroutine(SimulateAttck(circle));

                    lastSlash = Time.time;
                }
            }
        }
    }

    protected override void Drop()
    {
        PickupManager.instance.SpawnLootBox(transform.position);
    }

    protected override void Move()
    {
        MoveMotor((player.transform.position - transform.position).normalized);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(xOffset, yOffset) * transform.localScale.x/2, attackRange);
    }

    IEnumerator SimulateAttck(Vector3 circle)
    {
        anim.SetTrigger("Slash");

        yield return new WaitForSeconds(0.3f);

        Collider2D[] inRange = Physics2D.OverlapCircleAll(circle, attackRange);

        foreach (Collider2D coll in inRange)
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
                        damageToDeal = slashDamage - reduceDamagebyBy
                    };
                }


                 coll.SendMessage("ReceiveDamage", damage);

                 HurtEffect player = coll.gameObject.GetComponent<HurtEffect>();
                 player.Flash();
                
            }
        }
    }

    protected void Teleport()
    {

    }
}
