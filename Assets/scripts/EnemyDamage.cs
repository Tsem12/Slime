using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Animator animator;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private bool isAttacking = false;
    private bool isDead;


    private void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttacking == false && !isDead)
        {
            StartCoroutine(Damage());
        }

    }
     
    public void EnemyDealDamage(int damage)
    {
        isDead = gameObject.GetComponentInParent<EnemyPatrol>().isDead;
        if (!isDead)
        {
            playerHealth = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(10);
        }

    }

    IEnumerator Damage()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }
}
