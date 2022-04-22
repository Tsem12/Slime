using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Animator animator;
    private PlayerHealth playerHealth;
    private bool isAttacking = false;


    private void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttacking == false)
        {
            StartCoroutine(Damage());
            Debug.Log("qjlsdf");
        }

    }

    public void EnemyDealDamage(int damage)
    {
        playerHealth = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(10);

    }

    IEnumerator Damage()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }
}
