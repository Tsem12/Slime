using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Collider2D bc;
    private Transform playerSpawn;
    private GameObject player;
    private Animator animator;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            animator = player.GetComponent<Animator>();
            player.GetComponent<Rigidbody2D>().freezeRotation = true;
            animator.SetTrigger("Death");
        }

        
    }

}
