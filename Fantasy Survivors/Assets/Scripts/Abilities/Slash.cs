using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float slashRange;

    private Animator anim;


    public void SlashEffect(float damageToDeal)
    {
        // play slash animation here
        anim = GetComponent<Animator>();
        anim.SetTrigger("Slash");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, slashRange);

        foreach(Collider2D coll in hits)
        {
            if (coll.gameObject.CompareTag("Enemy"))
            {
                Damage damage = new Damage
                {
                    damageToDeal = damageToDeal
                };

                coll.gameObject.SendMessage("ReceiveDamage", damage);

                string msg = damageToDeal.ToString();

                GameManager.instance.ShowText(msg, 22, Color.white, coll.transform.position, Vector3.zero, 1f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, slashRange);
    }
}
