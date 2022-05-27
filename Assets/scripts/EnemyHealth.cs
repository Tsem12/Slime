using System.Collections;
using UnityEngine;
using TMPro;
public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public Animator animator;
    private EnemyDamage enemyDamage;

    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        enemyDamage = GetComponentInChildren<EnemyDamage>();
        healthBar.SetMawHealth(enemyHealth);
    }

    private void Update()
    {
    }

    public void TakeDamage(int damageAmount, float knockbackX, float knockbackY)
    {
        enemyHealth -= damageAmount;
        healthBar.SetHealth(enemyHealth);
        KnockBack(knockbackX, knockbackY);
        if (enemyHealth <= 0)
        {
            animator.SetBool("Death", true);
            this.GetComponent<EnemyPatrol>().isDead = true;
            this.GetComponentInChildren<EnemyDamage>().enabled = false;
            this.GetComponentInChildren<AbsorptionEnemy>().enabled = true;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        }
    }

    public void KnockBack(float xValue, float yValue)
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(xValue, yValue));
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
