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
        isAttacking = false;
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
        GameManager.isInputEnable = false;
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
        GameManager.isInputEnable = true;

    }

    void DealDamage()
    {
        if(canDamage == true)
        {
            enemyTarget.TakeDamage(1);
            enemyTarget.gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(1.5f, 1.5f);
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
