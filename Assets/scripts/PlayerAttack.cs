using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool isAttacking;
    private Animator animator;
    private bool canDamage;
    private EnemyHealth enemyTarget;
    public Rigidbody2D rb;
    public bool canAttack = true;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        isAttacking = false;
        canAttack = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.isInputEnable == true && isAttacking == false && canAttack == true)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        GameManager.isInputEnable = false;
        isAttacking = true;
        rb.velocity = new Vector2(0.0f, 0.0f);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
        GameManager.isInputEnable = true;

    }

    void DealDamage()
    {
        if(canDamage == true)
        {
            enemyTarget.TakeDamage(10);
            //if (rb.velocity.x > Mathf.Epsilon)
                //enemyTarget.gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(1.5f, 0f);
           // else
                //enemyTarget.gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyTarget = collision.GetComponentInParent<EnemyHealth>();
            canDamage = true;
        } else
        {
            canDamage = false;
        }

    }
}
