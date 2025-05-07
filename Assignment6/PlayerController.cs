using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement speeds and force settings
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 20f;
    public float dashDuration = 0.2f;

    // Private variables
    private Rigidbody2D rb;
    private bool isGrounded;         // Is player touching the ground?
    private bool isDashing;          // Is player currently dashing?
    private float dashTimeLeft;      // Time remaining in the dash
    private Vector2 dashDirection;   // Direction of the dash

    // Ground check stuff
    public Transform groundCheck;        // Empty GameObject under player
    public float groundCheckRadius = 0.1f;  // How big the ground check area is
    public LayerMask groundLayer;        // What counts as "ground"?

    void Start()
    {
        // Grab the Rigidbody2D from the Player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If dashing, override all movement temporarily
        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;

            // If dash time runs out, stop the dash
            if (dashTimeLeft <= 0f)
            {
                isDashing = false;
                rb.velocity = Vector2.zero;
            }

            return; // Exit so we don’t process normal movement while dashing
        }

        // Get horizontal movement input (-1 for left, 1 for right)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Set horizontal movement while keeping current Y velocity
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Check if we're standing on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // If space is pressed and we’re on the ground, jump!
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // If Left Shift is pressed, start a dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;

            // Dash in the direction the player is moving or facing
            dashDirection = new Vect
        }

    }
}