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
    public int playerDamage = 10;

    private Animator animator;
    private EnemyHealth enemyTarget ;
    private BossHealth bossTarget ;
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
            if(enemyTarget != null)
                enemyTarget.TakeDamage(playerDamage, knockBackX, knockBackY);
            if (bossTarget != null)
                bossTarget.TakeDamage(playerDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fly" || collision.tag == "Human" || collision.tag == "Golem")
        {
            enemyTarget = collision.GetComponentInParent<EnemyHealth>();
            canDamage = true;
        }
        else if (collision.tag == "Boss")
        {
            bossTarget = collision.GetComponentInParent<BossHealth>();
            canDamage = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Fly" || collision.tag != "Human" || collision.tag != "Golem" || collision.tag !="Boss")
        {
            enemyTarget = null;
            bossTarget = null;
            canDamage = false;
        }
    }
}
