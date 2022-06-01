using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 200;

    public bool isInVulnerable;
    public bool isPhase2;
    public bool stopted;
    public HealthBar healthBar;
    [SerializeField] private BossGate gateToActivate;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        healthBar.SetMawHealth(200);
    }

    public void TakeDamage(int damage)
    {
        if (isInVulnerable)
            return;

        health -= damage;
        healthBar.SetHealth(health);

        if(health < 100 && !isPhase2)
        {
            animator.SetTrigger("Regen");
            isPhase2 = true;
            isInVulnerable = true;
        }
        
        if(health < 0)
        {
            animator.SetTrigger("Dead");
            gateToActivate.isBossDefeated = true;
        }

        //Debug.Log(health);
    }

    public void Regen()
    {
        this.GetComponent<SpriteRenderer>().color = new Color32(43, 97, 195, 255);
        StartCoroutine(RegenCycle());
    }

    private IEnumerator RegenCycle()
    {
        while(health < 200 && stopted == false)
        {
            health += 5;
            healthBar.SetHealth(health);
            yield return new WaitForSeconds(0.5f);

        }
        stopted = true;
        animator.SetTrigger("InitSecondPhase");
    }

    public void Vulnerable()
    {
        isInVulnerable = false;
    }
}
