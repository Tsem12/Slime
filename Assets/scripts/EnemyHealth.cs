using System.Collections;
using UnityEngine;
using TMPro;
public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public Animator animator;
    private EnemyDamage enemyDamage;

    private void Start()
    {
        enemyDamage = GetComponentInChildren<EnemyDamage>();
    }

    private void Update()
    {
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        if (enemyHealth <= 0)
        {
            animator.SetBool("Death", true);
            this.GetComponent<EnemyPatrol>().isDead = true;
            this.GetComponentInChildren<EnemyDamage>().enabled = false;
            this.GetComponentInChildren<AbsorptionEnemy>().enabled = true;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        }
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
