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
    private int chargeLvl = 0;
    private Animator animator;
    private float postionCheck;
    private bool isDashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
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
                        GameManager.isInputEnable = true;
                        chargeLvl = 0;
                        isDashing = false;
                    }
                    break;
                case 2:
                    if (postionCheck - newPos <= -3f || postionCheck - newPos >= 3f)
                    {
                        rb.velocity = new Vector2(0f, 0f);
                        GameManager.isInputEnable = true;
                        chargeLvl = 0;
                        isDashing = false;
                    }
                    break;
            }

            Debug.Log(chargeLvl);
        }



        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Charge");
            chargingParticule.SetActive(true);
        }

        if (Input.GetMouseButton(1))
        {
            dashCharge -= Time.deltaTime;
            GameManager.isInputEnable = false;
            rb.velocity = new Vector2(0f, 0f);

            if (dashCharge <= 2)
                chargeParticule.SetActive(true);

            if (dashCharge <= 0)
                chargeParticule2.SetActive(true);


        }

        if (Input.GetMouseButtonUp(1))
        {
            if (dashCharge > 2)
            {
                if (playerAttack.isAttacking == false && playerAttack.canAttack == true)
                {
                    playerAttack.LauchAttack();
                }

            }

            else if (dashCharge <= 2 && dashCharge > 0)
            {
                Dash( 200f);
                chargeLvl = 1;
                postionCheck = this.transform.position.x;
            }

            else if (dashCharge <= 0)
            {
                Dash(300f);
                chargeLvl = 2;
                postionCheck = this.transform.position.x;

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

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyable") && chargeLvl > 1)
        {
            collision.gameObject.SetActive(false);

            //rb.velocity = new Vector2(0f, 0f);
            GameManager.isInputEnable = true;
            //chargeLvl = 0;
            isDashing = false;
        }  
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "GraphicsGolemBoss" && chargeLvl > 0)
        {
            collision.GetComponent<BossHealth>().stopted = true;
        }
    }
}
