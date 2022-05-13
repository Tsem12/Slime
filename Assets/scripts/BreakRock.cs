using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRock : MonoBehaviour
{
    public Sprite chargeSprite;

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
        }


        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Charge");
        }

        if (Input.GetMouseButton(1))
        {
            dashCharge -= Time.deltaTime;
            GameManager.isInputEnable = false;
            rb.velocity = new Vector2(0f, 0f);

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
                Dash(1.5f, 200f);
                chargeLvl = 1;
                postionCheck = this.transform.position.x;
            }

            else if (dashCharge <= 0)
            {
                Dash(3f, 300f);
                chargeLvl = 2;
                postionCheck = this.transform.position.x;

            }

            dashCharge = 3f;
        }
    }

    private void Dash(float range, float speed)
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
        if (collision.CompareTag("Destroyable") && chargeLvl > 0)
        {
            collision.gameObject.SetActive(false);
            switch (chargeLvl)
            {
                case 1:
                    rb.velocity = new Vector2(0f, 0f);
                    GameManager.isInputEnable = true;
                    chargeLvl = 0;
                    isDashing = false;
                    break;

            }
        }
    }
}
