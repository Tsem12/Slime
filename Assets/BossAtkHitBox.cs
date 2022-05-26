using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtkHitBox : MonoBehaviour
{
    [HideInInspector] public bool isInRange;
    [HideInInspector] public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            player = collision.gameObject;
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
}
