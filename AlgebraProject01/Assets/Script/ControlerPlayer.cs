using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlerPlayer : MonoBehaviour
{
    private float moveSpeed = 185;
    private float jumpForce = 250;

    private bool isJumping;
    private bool isGrounded;
    public bool inRangeToClimb;
    public bool isClimbing;

    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRendererPlayer;
    [SerializeField] private RotationManager rotationManager;
    public List<backgroundFloorTrigger> backgroundFloorManagerList;

    private float horizontalMouvement;
    private float verticalMouvement;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if(groundCheckLeft == null || groundCheckRight == null || rb == null || spriteRendererPlayer == null || rotationManager == null)
        {
            Debug.LogError("SerializeField Missing");
        }

        
        foreach (GameObject backgroundfloor in GameObject.FindGameObjectsWithTag("BackgroundFloor"))
        {
            backgroundFloorTrigger bgft = backgroundfloor.GetComponent<backgroundFloorTrigger>();
            if (bgft == null)
            {
                Debug.LogAssertion("backgroundFloorTrigger is not found in " + backgroundfloor.name + " at " + backgroundfloor.transform.position);
            }
            backgroundFloorManagerList.Add(bgft);
        }
    }
    void Update()
    {
        if(rotationManager.isRotating) { return;}


        // Mouvement player such as jump and moving left/right
        isGrounded = false;
        Collider2D hit = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        if(hit != null && hit.tag != "NotGround" && hit.tag != "BackgroundFloor")
        {
            isGrounded = true;
        }
        


        horizontalMouvement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMouvement = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;

        if(horizontalMouvement > 0)
        {
            spriteRendererPlayer.flipX = false;
        }
        else if(horizontalMouvement < 0)
        {
            spriteRendererPlayer.flipX = true;
        }

        

        
    }

    private void FixedUpdate()
    {
        if (rotationManager.isRotating) { return; }

        if (Input.GetAxis("Vertical") > 0f && isGrounded && isJumping == false && inRangeToClimb == false)
        {
            isJumping = true;
        }
        else if(Input.GetAxis("Vertical") != 0f && inRangeToClimb)
        {
            isClimbing = true;
            if(Input.GetAxis("Vertical") < 0f)
            {
                foreach(backgroundFloorTrigger bgft in backgroundFloorManagerList)
                {
                    bgft.DisactiveGround();
                }
                
            }
            
        }

        MovePlayer(horizontalMouvement, verticalMouvement);

        
    }

    public void MovePlayer(float _horizontalMouvement,float _verticalMouvement)
    {
        Vector3 targetVelocity;
        if(isClimbing)
        {
            targetVelocity = new Vector2(_horizontalMouvement, _verticalMouvement);
        }
        else
        {
            targetVelocity = new Vector2(_horizontalMouvement, rb.velocity.y);
        }
        
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
        

    }
}
