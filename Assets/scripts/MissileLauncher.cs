using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public Sprite chargeSprite;
    public GameObject missile;
    public GameObject chargeParticule;
    public GameObject chargeParticule2;
    public MissileBehaviour missileBehaviour;

    private Rigidbody2D rb;
    private PlayerAttack playerAttack;
    private Animator animator;
    private int chargeLvl = 0;
    private bool isShooting;
    private float dashCharge = 3f;
    private float postionCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isShooting == true)
        {
            postionCheck = Mathf.Abs(postionCheck);
            float newPos = Mathf.Abs(rb.transform.position.x);

            switch (chargeLvl)
            {
                case 1:
                    if (postionCheck - newPos <= -1.5f || postionCheck - newPos >= 1.5f)
                    {
                        Debug.Log("pan");
                        GameManager.isInputEnable = true;
                        chargeLvl = 0;
                        isShooting = false;
                    }
                    break;
                case 2:
                    if (postionCheck - newPos <= -3f || postionCheck - newPos >= 3f)
                    {
                        Debug.Log("pan2");
                        GameManager.isInputEnable = true;
                        chargeLvl = 0;
                        isShooting = false;
                    }
                    break;
            }
        }
 

        





        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("ChargeMissile");
            missile.SetActive(true);
            missileBehaviour.enabled = true;
        }

        if (Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(0f, 0f);
            dashCharge -= Time.deltaTime;
            GameManager.isInputEnable = false;

            if (dashCharge <= 2)
                chargeParticule.SetActive(true);

            if (dashCharge <= 0)
                chargeParticule2.SetActive(true);


        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            missileBehaviour.enabled = false;
            missile.SetActive(false);

            if (dashCharge > 2)
            {
                if (playerAttack.isAttacking == false && playerAttack.canAttack == true)
                {
                    playerAttack.LauchAttack();
                }

            }

            else if (dashCharge <= 2 && dashCharge > 0)
            {
                Dash(200f);
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
            chargeParticule.SetActive(false);
            chargeParticule2.SetActive(false);

        }

    }

    private void Dash(float speed)
    {

        isShooting = true;

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
                    isShooting = false;
                    break;

            }
        }
    }
}
