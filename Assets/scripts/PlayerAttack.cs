using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float knockBackX = 200000000000000000000000000000000000000.5f;
    public float knockBackY = 200000000000000000000000000000000000000.5f;
    public SwitchCharacter switchCharacter;
    private Rigidbody2D rb;
    public bool isAttacking;
    public bool canAttack = true;
    public bool isFly;

    private Animator animator;
    private EnemyHealth enemyTarget ;
    private bool canDamage;

    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
        isAttacking = false;
        canAttack = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.isInputEnable == true && isAttacking == false && canAttack == true && isFly == false)
        {
            LauchAttack();
        }
    }

    public void LauchAttack()
    {
            StartCoroutine(Attack());
    }

    public IEnumerator Attack()
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
            enemyTarget.TakeDamage(10, knockBackX, knockBackY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fly" || collision.tag == "Human" || collision.tag == "Golem")
        {
            enemyTarget = collision.GetComponentInParent<EnemyHealth>();
            canDamage = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Fly" || collision.tag != "Human" || collision.tag != "Golem")
        {
            enemyTarget = null;
            canDamage = false;
        }
    }
}
