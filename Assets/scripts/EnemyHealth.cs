using System.Collections;
using UnityEngine;

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
        if (enemyHealth <= 0)
        {
            animator.SetBool("Death", true);
            gameObject.GetComponent<EnemyPatrol>().enabled = false;

        }
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
    }

    public void DealDamage()
    {
        enemyDamage.EnemyDealDamage(10);
    }




}
