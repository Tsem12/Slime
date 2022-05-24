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
    [SerializeField]
    private float coolDown;
    private Rigidbody2D rigidbody;
    private bool canAttack;

    private void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        rigidbody = GetComponentInParent<Rigidbody2D>();
    }
    private void Update()
    {
        if (canAttack == true && isAttacking == false)
        {
            rigidbody.velocity = Vector3.zero;
            StartCoroutine(Damage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttacking == false && !isDead)
        {
            canAttack = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canAttack = false;
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
        yield return new WaitForSeconds(coolDown);
        isAttacking = false;
    }
}
