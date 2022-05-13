using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public float airResistance;

    public bool isGrounded;
    public bool isSlime;
    public bool isHuman;
    public bool isFly;
    public bool isRenf;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    private float horizontalMovement;
    //private bool canDoubbleJump;
    private bool canPlane = true;
    private BoxCollider2D atkHitBox;
    private bool isJumping;
    private float attackBoxPos;

    private void Start()
    {
        atkHitBox = gameObject.GetComponent<BoxCollider2D>();
        attackBoxPos = this.GetComponent<BoxCollider2D>().offset.x;
    }

    void Update()
    {
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");
            isJumping = true;
            canPlane = false;
        }

        Flip(rb.velocity.x);
        float charactervelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", charactervelocity);

        if (isGrounded == true)
        {
            //canDoubbleJump = true;
            animator.SetBool("IsGrounded", true);
        }

        /*if (isGrounded == true && isJumping == true)
        {
            animator.SetBool("IsGrounded", true);
            isJumping=false;
        }*/

        else
        {
            animator.SetBool("IsGrounded", false);
        }

        if (Input.GetButton("Jump") && isGrounded == false && canPlane == true && GameManager.isInputEnable == true && isFly == true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }


    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (GameManager.isInputEnable == true)
            MovePlayer(horizontalMovement);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);


    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }


    }


    void Flip(float _velocity)
    {
        

        if (_velocity > 0.1f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (_velocity < -0.1f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    /*private void DoubbleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0.0f);

        rb.AddForce(new Vector2(0f, jumpForce));
        isJumping = false;
        canDoubbleJump = false;
        StartCoroutine(waiter());
    } */

    private void Plane()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }


    public void EndJump()
    {
        canPlane = true;
    }
}
