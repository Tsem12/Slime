using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking;
    private Animator animator;
    private bool canDamage;
    private EnemyHealth enemyTarget;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.isInputEnable == true && isAttacking == false)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;

    }

    void DealDamage()
    {
        if(canDamage == true)
        {
            enemyTarget.TakeDamage(10);
            enemyTarget.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 10f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyTarget = collision.GetComponent<EnemyHealth>();
            canDamage = true;
        } else
        {
            canDamage = false;
        }

    


    }
}
