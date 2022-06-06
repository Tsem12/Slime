using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health = 200;
    [HideInInspector] public bool isDashed;

    public bool isInVulnerable;
    public bool isPhase2;
    public bool stopted;
    public HealthBar healthBar;
    [SerializeField] private Sprite crakSprite;
    [SerializeField] private GameObject fillGameobject;
    [SerializeField] private BossGate gateToActivate;
    [SerializeField] private GameObject healingPatricule;
    private bool isDead;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        healthBar.SetMawHealth(200);
    }

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            animator.SetBool("Death", true);
            animator.SetBool("StartDeath", true);
            isDead = true;
            gateToActivate.isBossDefeated = true;
            GetComponent<AbsorptionEnemy>().isDead = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInVulnerable)
            return;

        health -= damage;
        healthBar.SetHealth(health);

        if(health <= 100 && !isPhase2)
        {
            animator.SetTrigger("Regen");
            isPhase2 = true;
            isInVulnerable = true;
        }
        

        //Debug.Log(health);
    }

    public void Regen()
    {
        GetComponent<SpriteRenderer>().color = new Color32(43, 97, 195, 255);
        StartCoroutine(RegenCycle());
    }

    private IEnumerator RegenCycle()
    {
        while(health < 200 && stopted == false)
        {
            fillGameobject.GetComponent<Image>().sprite = crakSprite;
            fillGameobject.GetComponent<Image>().color = Color.white;
            healingPatricule.SetActive(true);
            health += 5;
            healthBar.SetHealth(health);
            yield return new WaitForSeconds(0.5f);

        }
        stopted = true;
        fillGameobject.GetComponent<Image>().sprite = null;
        fillGameobject.GetComponent<Image>().color = new Color32(27, 56, 109, 255);
        healingPatricule.SetActive(false);
        animator.SetTrigger("InitSecondPhase");
    }

    public void Vulnerable()
    {
        isInVulnerable = false;
    }

    public void ResetFight()
    {
        StopAllCoroutines();
        health = 200;
        healthBar.SetHealth(health);
        isPhase2 = false;
        stopted = false;
        fillGameobject.GetComponent<Image>().color = new Color32(128, 0, 11, 255);
        fillGameobject.GetComponent<Image>().sprite = null;
        GetComponent<SpriteRenderer>().color = Color.white;
        healingPatricule.SetActive(false);

        if(animator != null)
            animator.SetTrigger("Reset");
    }
}
