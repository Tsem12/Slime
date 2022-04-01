using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Collider2D bc;
    private Transform playerSpawn;
    private GameObject player;
    private Animator animator;
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody2D>().freezeRotation = true;
            StartCoroutine(Death());
        }

        
    }

    IEnumerator Death()
    {
        animator = player.GetComponent<Animator>();
        Debug.Log("cmarche1");
        animator.SetBool("Death", true);
        Debug.Log("cmarche2");
        yield return new WaitForSeconds(0.3f);
        Debug.Log("cmarche3");
        animator.SetBool("Death", false);
        player.transform.position = playerSpawn.position;
    }
}
