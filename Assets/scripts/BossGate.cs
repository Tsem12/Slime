using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public bool isBossDefeated;
    private Animator animator;

    private BoxCollider2D bc;
    private BoxCollider2D parentBc;
    private bool isOpen;
    [SerializeField] private GameObject elevatorObject;
    

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        parentBc = GetComponentInParent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBossDefeated)
        {
            if (elevatorObject != null)
                elevatorObject.GetComponent<Elevator>().isON = true;
        }
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

        

        }

       
    }

}

