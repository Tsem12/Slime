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
            gameObject.GetComponent<EnemyPatrol>().isDead = true;
            gameObject.GetComponentInChildren<EnemyDamage>().enabled = false;

        }
    }

    public void DealDamage()
    {
        enemyDamage.EnemyDealDamage(10);
    }




}
