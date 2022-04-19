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
    private BoxCollider2D atkHitBox;

    private void Start()
    {
        atkHitBox = gameObject.GetComponent<BoxCollider2D>();
    }


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
        }
        else
            animator.SetBool("IsGrounded", false);

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
            atkHitBox.offset = new Vector2(0.517123f, atkHitBox.offset.y);
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
            atkHitBox.offset = new Vector2(-0.517123f, atkHitBox.offset.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


    IEnumerator waiter()
    {
        animator.SetBool("Jump", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Jump", false);
    }

}
