using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    private Transform playerSpawn;
    private Animator animator;


    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDeath()
    {
        animator.SetTrigger("Death");
    }

    public void Death()
    {
        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent <PlayerHealth>();
        gameObject.transform.position = playerSpawn.position;
        animator.SetTrigger("SwitchOut");
        playerHealth.currentHealth = playerHealth.maxHealth;
        playerHealth.healthBar.SetHealth(playerHealth.currentHealth);
    }


}
