using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float knockBackX;
    public float knockBackY;
    public SwitchCharacter switchCharacter;
    private Rigidbody2D rb;
    public bool isAttacking;
    public bool canAttack = true;
    public bool isFly;
    public int playerDamage = 100;

    private Animator animator;
    private EnemyHealth enemyTarget ;
    private BossHealth bossTarget ;
    private bool canDamage;
    private PlayerMovement playerMovement;

    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
        isAttacking = false;
        canAttack = true;
        playerMovement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.isInputEnable == true && isAttacking == false && canAttack == true && isFly == false)
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
        GameManager.instance.isInputEnable = false;
        isAttacking = true;
        rb.velocity = new Vector2(0.0f, 0.0f);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
        GameManager.instance.isInputEnable = true;

    }

    void DealDamage()
    {
        if(canDamage == true)
        {
            if(enemyTarget != null)
                enemyTarget.TakeDamage(playerDamage, knockBackX, knockBackY, playerMovement.direction);
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
        if (collision.tag == "Fly" || collision.tag == "Human" || collision.tag == "Golem" || collision.tag == "Boss")
        {
            enemyTarget = null;
            bossTarget = null;
            canDamage = false;
        }
    }
}
