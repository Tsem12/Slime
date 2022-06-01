using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private SwitchCharacter switchCharacter;

    public HealthBar healthBar;
    void Start()
    {
        switchCharacter = FindObjectOfType<SwitchCharacter>();
        currentHealth = maxHealth;
        healthBar.SetMawHealth(maxHealth);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(50);
        }


    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(AnimantionDamage());
        if (currentHealth <= 0)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().StartDeath();

    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }

    private IEnumerator AnimantionDamage()
    {
        SpriteRenderer spriteRenderer = switchCharacter.activeCharacter.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.enabled = true;
    }


}
