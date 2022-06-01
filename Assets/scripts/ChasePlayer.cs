using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float chaseSpeed = 2f;
    public LayerMask collisionLayers;

    private Rigidbody2D rb;

    [SerializeField] private Transform mob;
    [SerializeField] private float range = 5;
    [SerializeField] private float yOffsetCorrection;
    [SerializeField] private BoxCollider2D hitBox;
    private EnemyPatrol enemyPatrol;

    private bool isInArea;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        enemyPatrol = GetComponentInChildren<EnemyPatrol>();
    }

    private void Update()
    {

        Debug.DrawRay(new Vector2(mob.position.x - hitBox.size.x, mob.position.y - yOffsetCorrection), -mob.right * range, Color.red);
        Debug.DrawRay(new Vector2(mob.position.x + hitBox.size.x, mob.position.y - yOffsetCorrection), mob.right * range, Color.green);

        if(isInArea == true && !enemyPatrol.isDead)
        {

            rb.velocity = Vector2.zero;
            RaycastHit2D[] hitLeft = Physics2D.RaycastAll(new Vector2(mob.position.x - hitBox.size.x , mob.position.y - yOffsetCorrection), -mob.right, range, collisionLayers);
            RaycastHit2D[] hitRight = Physics2D.RaycastAll(new Vector2(mob.position.x + hitBox.size.x, mob.position.y - yOffsetCorrection), mob.right, range, collisionLayers);

            if (hitLeft != null)
            {
                foreach (RaycastHit2D hit in hitLeft)
                {
                    if (hit.collider.tag == "Player")
                    {
                        rb.velocity = new Vector2( - chaseSpeed, 0f);
                        //Debug.Log("Detected Left");
                    }
                        
                }
            }
            if (hitRight != null)
            {
                foreach (RaycastHit2D hit in hitRight)
                {
                    if (hit.collider.tag == "Player")
                    {
                        rb.velocity = new Vector2(chaseSpeed, 0f);
                        //Debug.Log("Detected Right");
                    }
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInChildren<EnemyPatrol>().isPatrol = false;
            isInArea = true;
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            GetComponentInChildren<EnemyPatrol>().isPatrol = true;
            isInArea = false;
        }

    }
}
