using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtkHitBox : MonoBehaviour
{
    [HideInInspector] public bool isInRange;
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject missile;
    [HideInInspector] public bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            player = collision.gameObject;
        }
        if (this.name == "Missile" && collision.CompareTag("platform"))
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            player = null;
        }
    }

    /// <summary>
    /// Active or desactive boss attack hit box
    /// </summary>
    /// <param name="isActive"></param>
    public void BoxColliderEnabler(int isActive)
    {
        if(isActive == 1)
            GetComponent<BoxCollider2D>().enabled = true;
        else
            GetComponent<BoxCollider2D>().enabled = false;
    }
}
