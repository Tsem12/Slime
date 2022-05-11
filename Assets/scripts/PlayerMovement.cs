using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public float airResistance;

    private bool isJumping;
    public bool isGrounded;
    public bool isSlime;
    public bool isHuman;
    public bool isFly;
    public bool isRenf;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float attackBoxPos;

    private Vector3 velocity = Vector3.zero;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    private float horizontalMovement;
    //private bool canDoubbleJump;
    private bool canPlane = true;
    private BoxCollider2D atkHitBox;

    private void Start()
    {
        atkHitBox = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);
        float charactervelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", charactervelocity);

        if (isGrounded == true)
        {
            //canDoubbleJump = true;
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);
        }

        if (Input.GetButton("Jump") && isGrounded == false && canPlane == true && GameManager.isInputEnable == true && isFly == true)
        {
            /*Plane();
            Debug.Log("plane");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;*/

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
            StartCoroutine(waiter());
        }


    }


    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
            atkHitBox.offset = new Vector2(attackBoxPos, atkHitBox.offset.y);
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
            atkHitBox.offset = new Vector2(-attackBoxPos, atkHitBox.offset.y);
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

    /*private void Plane()
    {
        rb.AddForce(new Vector2(rb.velocity.x, airResistance));
    }*/


    IEnumerator waiter()
    {
        animator.SetBool("Jump", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Jump", false);
    }
}
