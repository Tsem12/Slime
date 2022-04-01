using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject cpActive;
    [SerializeField] private GameObject cpInActive;
    [SerializeField] private Collider2D cl;

    private Transform playerSpawn;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void Update()
    {
        if (playerSpawn.position == transform.position)
            StartCoroutine(waiter());
        else
        {
            animator.SetBool("Active", false);
            cpActive.SetActive(false);
            cpInActive.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
        }
    }

    IEnumerator waiter()
    {
        animator.SetBool("Active", true);
        yield return new WaitForSeconds(1.5f);
        cpActive.SetActive(true);
        cpInActive.SetActive(false);
    }
}
