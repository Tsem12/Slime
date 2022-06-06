using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private SwitchCharacter switchCharacter;
    [SerializeField] GameObject healingParticule;

    public HealthBar healthBar;
    void Start()
    {
        switchCharacter = FindObjectOfType<SwitchCharacter>();
        currentHealth = maxHealth;
        healthBar.SetMawHealth(maxHealth);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(50);
        }

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }


    }

    public void TakeDamage(int damage)
    {
        if(!GameManager.instance.isGodMod)
            currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(AnimantionDamage());
        if (currentHealth <= 0)
        {
            SwitchCharacter.instance.activeCharacter.GetComponent<PlayerDeath>().StartDeath();
            SwitchCharacter.instance.activeCharacter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(Healing());
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

    private IEnumerator Healing()
    {
        healingParticule.SetActive(true);
        yield return new WaitForSeconds(1);
        healingParticule.SetActive(false);
    }


}
