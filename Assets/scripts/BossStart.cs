using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject boss;
    public Collider2D cl;


    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Init());
            
        }
    }

        IEnumerator Init()
        {
            boss.SetActive(true);
            yield return new WaitForSeconds(1);
            animator.SetBool("Entery", true);
            yield return new WaitForSeconds(1);
            animator.SetBool("Idle", true);
        }
}
