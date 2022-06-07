using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRock : MonoBehaviour
{
    public GameObject chargingParticule;
    public GameObject chargeParticule;
    public GameObject chargeParticule2;

    private Rigidbody2D rb;
    private float dashCharge = 3f;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private int chargeLvl = 0;
    private Animator animator;
    private float postionCheck;
    [HideInInspector] public bool isDashing;
    private List<GameObject> enemiesDashed = new List<GameObject>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(isDashing == true)
        {
            postionCheck = Mathf.Abs(postionCheck);
            float newPos = Mathf.Abs(rb.transform.position.x);
            
            switch (chargeLvl)
            {
                case 1:
                    if(postionCheck - newPos <= -1.5f || postionCheck - newPos >= 1.5f)
                    {
                        rb.velocity = new Vector2(0f, 0f);
                        GameManager.instance.isInputEnable = true;
                        chargeLvl = 0;
                        isDashing = false;
                        GameManager.instance.canMove = true;
                        EmptyArray();
                    }
                    break;
                case 2:
                    if (postionCheck - newPos <= -3f || postionCheck - newPos >= 3f)
                    {
                        rb.velocity = new Vector2(0f, 0f);
                       GameManager.instance.isInputEnable = true;
                        chargeLvl = 0;
                        isDashing = false;
                        GameManager.instance.canMove = true;
                        EmptyArray();
                    }
                    break;
            }
        }



        if (Input.GetMouseButtonDown(1) && AbilitieManager.instance.canDash)
        {
            animator.SetTrigger("Charge");
            chargingParticule.SetActive(true);
        }

        if (Input.GetMouseButton(1) && AbilitieManager.instance.canDash)
        {
            dashCharge -= Time.deltaTime;
            GameManager.instance.isInputEnable = false;
            rb.velocity = new Vector2(0f, 0f);

            if (dashCharge <= 2)
                chargeParticule.SetActive(true);

            if (dashCharge <= 0)
                chargeParticule2.SetActive(true);


        }

        if (Input.GetMouseButtonUp(1) && AbilitieManager.instance.canDash)
        {
            if (dashCharge > 2)
            {
                if (playerAttack.isAttacking == false && playerAttack.canAttack == true)
                {
                    playerAttack.LauchAttack();
                    AbilitieManager.instance.StartCoolDownCoroutine(AbilitieManager.instance.GolemDash, 0.5f, 2);
                }

            }

            else if (dashCharge <= 2 && dashCharge > 0)
            {
                Dash( 200f);
                chargeLvl = 1;
                postionCheck = this.transform.position.x;
                AbilitieManager.instance.StartCoolDownCoroutine(AbilitieManager.instance.GolemDash, 1.5f, 2);
            }

            else if (dashCharge <= 0)
            {
                Dash(300f);
                chargeLvl = 2;
                postionCheck = this.transform.position.x;
                AbilitieManager.instance.StartCoolDownCoroutine(AbilitieManager.instance.GolemDash, 3f, 2);

            }

            dashCharge = 3f;
            chargingParticule.SetActive(false);
            chargeParticule.SetActive(false);
            chargeParticule2.SetActive(false);
        }
    }

    private void Dash(float speed)
    {
        if(transform.eulerAngles.y == 0)
        {
            rb.AddForce(new Vector2(speed, 0f));
        }
        else
        {
            rb.AddForce(new Vector2(-speed, 0f));
        }
        isDashing = true;
        playerAttack.LauchAttack();
        GameManager.instance.canMove = false;

    }

    private void EmptyArray()
    {
        foreach(GameObject mob in enemiesDashed)
        {
            if (mob.GetComponentInParent<EnemyHealth>() != null)
                mob.GetComponentInParent<EnemyHealth>().isDashed = false;

            if (mob.GetComponentInParent<BossHumanHealth>() != null)
                mob.GetComponentInParent<BossHumanHealth>().isDashed = false;

            if (mob.GetComponentInParent<BossHealth>() != null)
                mob.GetComponentInParent<BossHealth>().isDashed = false;
        }
        enemiesDashed.Clear();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyable") && chargeLvl == 1)
        {
            collision.gameObject.SetActive(false);

            rb.velocity = new Vector2(0f, 0f);
            GameManager.instance.isInputEnable = true;
            chargeLvl = 0;
            isDashing = false;
            GameManager.instance.canMove = true;
            EmptyArray();
        }

        if (collision.CompareTag("Destroyable") && chargeLvl == 2)
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "platform" || collision.tag == "Enemy")
        {
            rb.velocity = new Vector2(0f, 0f);
            GameManager.instance.isInputEnable = true;
            chargeLvl = 0;
            isDashing = false;
            GameManager.instance.canMove = true;
            EmptyArray();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "GraphicsGolemBoss" && chargeLvl > 0)
        {
            collision.GetComponent<BossHealth>().stopted = true;
        }

        if (collision.tag == "Fly" || collision.tag == "Human" || collision.tag == "Golem" && !collision.GetComponent<AbsorptionEnemy>().isDead)
        {
            if(collision.GetComponentInParent<EnemyHealth>().isDashed == false && chargeLvl > 0)
            {
                enemiesDashed.Add(collision.gameObject);

                if (collision.GetComponent<EnemyHealth>())
                {
                    collision.GetComponentInParent<EnemyHealth>().TakeDamage(10, 2, 2, playerMovement.direction);
                    collision.GetComponentInParent<EnemyHealth>().isDashed = true;
                }
            }
        }

        if(collision.tag == "Boss")
        {
            if (collision.GetComponent<BossHumanHealth>())
            {
                if (!collision.GetComponent<BossHumanHealth>().isDashed)
                {
                    collision.GetComponent<BossHumanHealth>().TakeDamage(15 * chargeLvl);
                    collision.GetComponentInParent<BossHumanHealth>().isDashed = true;
                }
            }

            else if (collision.GetComponent<BossHealth>())
            {
                if (!collision.GetComponent<BossHealth>().isDashed)
                {
                    collision.GetComponent<BossHealth>().TakeDamage(15 * chargeLvl);
                    collision.GetComponentInParent<BossHealth>().isDashed = true;
                }
            }
        }
    }

}
