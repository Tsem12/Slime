using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public Animator animator;

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




}
