using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    [SerializeField] private GameObject triger;
    [SerializeField] private Animator animator;

    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private BoxCollider2D parentBc;

    private 

    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("Open", true);
        Debug.Log(" c bon");
        parentBc.GetComponent<BoxCollider2D>().enabled = true;
        
        
    }
}

