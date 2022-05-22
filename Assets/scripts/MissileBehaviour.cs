using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    public PlayerMovement playerMouvement;

    private Rigidbody2D rb;
    [SerializeField]
    private Transform initPos;
    private Vector2 lookDirection;
    private float lookAngle;
    private float knockBackSpeed;
    public Animator animator;
    public bool isShooting = false;
    private bool knockback = false;
    private int chargeLvl;

    [SerializeField]
    private GameObject parent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (parent.transform.eulerAngles.y == 0 && isShooting == false)
        {
            if (lookAngle < 90 && lookAngle >= 0 || lookAngle > -90 && lookAngle <= 0)
                transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
            else if (lookAngle > 90)
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            else if (lookAngle < -90)
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            parent.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (parent.transform.eulerAngles.y == 180 && isShooting == false)
        {
            if (lookAngle > 90 && lookAngle <= 180 || lookAngle < -90 && lookAngle >= -180)
                transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
            else if (lookAngle < 90 && lookAngle > 0)
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            else if (lookAngle > -90 && lookAngle < 0)
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            parent.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(knockback == true)
            parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockBackSpeed, 0f));
    }

    public void Fire(float speed, int charge)
    {
        isShooting = true;
        if (parent.transform.eulerAngles.y == 0)
        {
            if (lookAngle < 90 && lookAngle >= 0 || lookAngle > -90 && lookAngle <= 0)
                rb.velocity = new Vector2(lookDirection.x * speed, lookDirection.y * speed);
            else if (lookAngle > 90)
                rb.velocity = new Vector2(0f, speed);
            else if (lookAngle < -90)
                rb.velocity = new Vector2(0f,  -speed);
            knockBackSpeed = -3f;
        }
        else
        {
            if (lookAngle > 90 && lookAngle <= 180 || lookAngle < -90 && lookAngle >= -180)
                rb.velocity = new Vector2(lookDirection.x * speed, lookDirection.y * speed);
            else if (lookAngle < 90 && lookAngle > 0)
                rb.velocity = new Vector2(0f, speed);
            else if (lookAngle > -90 && lookAngle < 0)
                rb.velocity = new Vector2(0f, -speed);
            knockBackSpeed = 3f;
        }       
        StartCoroutine(KnockBack());
        chargeLvl = charge;
    }


    private IEnumerator KnockBack()
    {
        playerMouvement.canFlip = false;
        knockback = true;
        yield return new WaitForSeconds(0.5f);
        knockback = false;
        animator.SetTrigger("Idle");
        playerMouvement.canFlip = true;

    }

    public void Init()
    {
        this.transform.position = initPos.position;
        rb.velocity = Vector2.zero;

    }

    private void Collision(float radius)
    {
        RaycastHit2D[] hits;
        hits = Physics2D.CircleCastAll(transform.position, radius - 0.2f, Vector2.right, radius - 0.2f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Destroyable")
                hit.collider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyable") && chargeLvl > 0 && isShooting == true)
        {
            knockback = false;
            animator.SetTrigger("Idle");
            playerMouvement.canFlip = true;
            Collision(chargeLvl);
            this.gameObject.SetActive(false);

        }
        else if(collision.tag != "Destroyable" && collision.tag != "Player" && collision.tag != "Untagged" && isShooting == true)
        {
            knockback = false;
            animator.SetTrigger("Idle");
            playerMouvement.canFlip = true;
            this.gameObject.SetActive(false);
            Debug.Log(collision.gameObject.name);
        }
    }

}
