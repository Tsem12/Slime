using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    [SerializeField] private GameObject triger;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator animatorElevator;

    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private BoxCollider2D parentBc;
    [SerializeField] private bool isOpen;

    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (isOpen == true)
            {
            animator.SetBool("Open", true);
            parentBc.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
            animator.SetBool("Open", true);
            parentBc.GetComponent<BoxCollider2D>().enabled = false;
            }

        StartCoroutine(ActiveElevator());

        }

       
    }

    IEnumerator ActiveElevator()
    {
        animatorElevator.SetBool("SetActive", true);
        yield return new WaitForSeconds(1);
        animatorElevator.SetBool("Active", true);
    }
}

