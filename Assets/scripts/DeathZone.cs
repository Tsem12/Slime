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
            player = SwitchCharacter.instance.activeCharacter;
            animator = player.GetComponent<Animator>();
            player.GetComponent<Rigidbody2D>().freezeRotation = true;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            animator.SetTrigger("Death");
        }

        
    }

}
