using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMawHealth(maxHealth);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage(20);
        }

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().StartDeath();

    }

}
