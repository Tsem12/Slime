using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRock : MonoBehaviour
{
    public Sprite chargeSprite;

    private Rigidbody2D rb;
    private float dashCharge = 5f;
    private PlayerAttack playerAttack;
    private BoxCollider2D collider;
    private int chargeLvl = 0;
    private Animator animator;
    private float postionCheck;
    private bool isDashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isDashing == true)
        {
            switch (chargeLvl)
            {
                case 1:
                    if(postionCheck - rb.transform.position.x <= -1.5f)
                    {
                        rb.velocity = new Vector2(0f, 0f);
                        GameManager.isInputEnable = true;
                        chargeLvl = 0;
                    }
                    break;
                case 2:
                    if (postionCheck - rb.transform.position.x <= -3f)
                    {
                        rb.velocity = new Vector2(0f, 0f);
                        GameManager.isInputEnable = true;
                        chargeLvl = 0;
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
            if (dashCharge > 3)
            {
                if (GameManager.isInputEnable == true && playerAttack.isAttacking == false && playerAttack.canAttack == true)
                {
                    playerAttack.LauchAttack();
                }

            }

            else if (dashCharge <= 3 && dashCharge > 0)
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

            dashCharge = 5f;
        }
    }

    private void Dash(float range, float speed)
    {
        rb.AddForce(new Vector2(speed, 0f));
        isDashing = true;

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
                    break;

            }
        }
    }
}
