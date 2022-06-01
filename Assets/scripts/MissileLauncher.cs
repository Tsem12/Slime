using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
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
                        GameManager.instance.isInputEnable = true;
                        chargeLvl = 0;
                        isShooting = false;
                    }
                    break;
                case 2:
                    if (postionCheck - newPos <= -3f || postionCheck - newPos >= 3f)
                    {
                        Debug.Log("pan2");
                        GameManager.instance.isInputEnable = true;
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
            missileBehaviour.Init();
            missileBehaviour.isShooting = false;
            rb.velocity = new Vector2(0f, 0f);
            GameManager.instance.isInputEnable = false;
        }

        if (Input.GetKey(KeyCode.X))
        {
            dashCharge -= Time.deltaTime;

            if (dashCharge <= 2)
                chargeParticule.SetActive(true);

            if (dashCharge <= 0)
                chargeParticule2.SetActive(true);


        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            if (dashCharge > 2)
            {
                missileBehaviour.Fire(2f,0);
            }

            else if (dashCharge <= 2 && dashCharge > 0)
            {
                missileBehaviour.Fire(5f, 1);
            }

            else if (dashCharge <= 0)
            {
                chargeLvl = 2;
                missileBehaviour.Fire(10f, 2);

            }

            dashCharge = 3f;
            chargeParticule.SetActive(false);
            chargeParticule2.SetActive(false);
            GameManager.instance.isInputEnable = true;

        }

    }

}
