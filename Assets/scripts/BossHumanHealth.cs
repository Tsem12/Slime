using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHumanHealth : MonoBehaviour
{
    public int health = 200;
    public HealthBar healthBar;
    [HideInInspector] public bool isDashed;
    [HideInInspector] public bool isInvunerable = true;

    [SerializeField] private BossGate gateToActivate;
    [SerializeField] private DialogueTrigger defeate;
    [SerializeField] private Transform StartPoint;
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
            isDead = true;
            gateToActivate.isBossDefeated = true;
            GetComponent<AbsorptionEnemy>().isDead = true;
            defeate.TrigerDialogue();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvunerable)
        {
            health -= damage;
            healthBar.SetHealth(health);
        }
    }

    public void ResetFight()
    {
        gateToActivate.ResetDoor();
        GetComponent<Animator>().SetBool("BossStart", false);
        GetComponentInParent<Rigidbody2D>().transform.position = StartPoint.position;
        GetComponent<TrigerTuto>().isDone = false;
        health = 200;
        healthBar.SetHealth(health);
        GetComponent<BoxCollider2D>().enabled = true;
        isInvunerable = true;
    }




}
