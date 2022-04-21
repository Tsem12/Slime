using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

    void Death()
    {
        gameObject.transform.position = playerSpawn.position;
        animator.SetTrigger("SwitchOut");
    }
}
