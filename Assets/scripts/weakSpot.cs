using System.Collections;
using UnityEngine;

public class weakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public Animator animator;
    public BoxCollider2D boxCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("Death", true);
            boxCollider.enabled = false;
            Destroy(objectToDestroy, 1.5f);
        }
    }
       

}
