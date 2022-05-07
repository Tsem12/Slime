using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{

    private Rigidbody2D rb;
    private Transform mob;
    private GameObject player;
    public float chaseSpeed = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mob = GetComponentInChildren<Transform>();
            rb = GetComponentInChildren<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
            GetComponentInChildren<EnemyPatrol>().enabled = false;
            Debug.Log("attack");

            RaycastHit2D hitLeft = Physics2D.Raycast(mob.position, Vector2.left);
            RaycastHit2D hitRight = Physics2D.Raycast(mob.position, Vector2.left);



            if (hitLeft == player)
            {
                //rb.velocity = new Vector2(chaseSpeed, 0);
                //transform.localScale = new Vector2(1, 1);
            }
            else
            {
                //rb.velocity = new Vector2(-chaseSpeed, 0);
                //transform.localScale = new Vector2(-1, 1);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInChildren<EnemyPatrol>().enabled = true;
        }

    }
}
