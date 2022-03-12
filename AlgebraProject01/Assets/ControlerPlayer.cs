using UnityEngine;

public class ControlerPlayer : MonoBehaviour
{
    private float moveSpeed = 250;
    private float jumpForce = 250;

    private bool isJumping;
    private bool isGrounded;

    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Rigidbody2D rb;


    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if(groundCheckLeft == null || groundCheckRight == null || rb == null)
        {
            Debug.LogError("SerializeField Missing");
        }
    }
    void FixedUpdate()
    {
        // Mouvement player such as jump and moving left/right
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);


        float horizontalMouvement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if(Input.GetAxis("Vertical") > 0f && isGrounded && isJumping == false)
        {
            isJumping = true;
        }

        MovePlayer(horizontalMouvement);
    }

    public void MovePlayer(float _horizontalMouvement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMouvement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
}
