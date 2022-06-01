using System.Collections;
using UnityEngine;
using TMPro;
public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public bool isDashed;

    public int enemyHealth;
    public Animator animator;
    private EnemyDamage enemyDamage;
    private EnemyPatrol enemyPatrol;
    private Rigidbody2D rb;
    private ChasePlayer chasePlayer;
    private SpriteRenderer spriteRenderer;


    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetMawHealth(enemyHealth);
        enemyPatrol = GetComponent<EnemyPatrol>();
        enemyDamage = GetComponentInChildren<EnemyDamage>();
        chasePlayer = GetComponentInParent<ChasePlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damageAmount, float knockbackX, float knockbackY, int direction)
    {
        if (!enemyPatrol.isDead)
        {
            enemyHealth -= damageAmount;
            healthBar.SetHealth(enemyHealth);
            if (enemyHealth <= 0)
            {
                animator.SetBool("Death", true);
                enemyPatrol.isDead = true;
                enemyDamage.enabled = false;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            StartCoroutine(KnockBack(knockbackX, knockbackY, direction));
        }
    }

    public IEnumerator KnockBack(float xValue, float yValue, int direction)
    {
        rb.AddForce(new Vector2(xValue * direction, yValue));
        spriteRenderer.color = Color.red;
        chasePlayer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        chasePlayer.enabled = true;
        spriteRenderer.color = Color.white;
    }

    public void DealDamage()
    {
        enemyDamage.EnemyDealDamage(10);
    }

    public void AnimatorOff()
    {
        animator.enabled = false;

    }




}
