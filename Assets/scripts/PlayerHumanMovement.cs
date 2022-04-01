using System.Collections;
using UnityEngine;

public class PlayerHumanMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    public bool isGrounded;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    private float horizontalMovement;
    private bool canDoubbleJump;


    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);
        float charactervelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", charactervelocity);

        if (isGrounded == true)
        {
            animator.SetBool("IsGrounded", true);
            canDoubbleJump = true;
        }
        else
            animator.SetBool("IsGrounded", false);

        if (Input.GetButtonDown("Jump") && isGrounded == false && canDoubbleJump == true)
            DoubbleJump();


    }


    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

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
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void DoubbleJump()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);

        rb.AddForce(new Vector2(0f, jumpForce));
        isJumping = false;
        canDoubbleJump = false;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        animator.SetBool("Jump", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Jump", false);
    }
}