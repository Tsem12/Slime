using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 200;

    public bool isInVulnerable;
    public bool isPhase2;
    public bool stopted;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(health);
        if (isInVulnerable)
            return;

        health -= damage;

        if(health < 100 && !isPhase2)
        {
            animator.SetTrigger("Regen");
            isPhase2 = true;
            isInVulnerable = true;
        }
        
        if(health < 0)
        {
            animator.SetTrigger("Dead");
        }

    }

    public void Regen()
    {
        this.GetComponent<SpriteRenderer>().color = new Color32(43, 97, 195, 255);
        StartCoroutine(RegenCycle());
    }

    private IEnumerator RegenCycle()
    {
        while(health < 200 || !stopted)
        {
            health += 5;
            Debug.Log(health);
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
