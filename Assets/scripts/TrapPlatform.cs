using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{

    [SerializeField] private GameObject parent;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private BoxCollider2D trigerArea;
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        bc.enabled = false;
        rb.constraints = RigidbodyConstraints2D.None;
        Destroy(parent, 2);
        }
    }
}
