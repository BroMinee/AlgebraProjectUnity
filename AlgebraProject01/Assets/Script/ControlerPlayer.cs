using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlerPlayer : MonoBehaviour
{
    private float moveSpeed = 185; // The player speed
    private float jumpForce = 250; // The player jump force

    private bool isJumping;
    private bool isGrounded;
    public bool inRangeToClimb;
    public bool isClimbing;
    [SerializeField] private bool hasDoubleJump = false;
    [SerializeField] private int nbJump = 0;
    public bool hasSlowFalling = false;

    public bool facingRight;
    [SerializeField] public Vector2 attackForce;


    [SerializeField] private Transform groundCheckLeft; // Point1 to check if the player is grounded
    [SerializeField] private Transform groundCheckRight; // Point2 to check if the player is grounded
    [SerializeField] private Rigidbody2D rb; // RigidBody of the player
    [SerializeField] private SpriteRenderer spriteRendererPlayer; // Graphic part of the player
    [SerializeField] private RotationManager rotationManager; // Rotation Manager of the level
    public List<backgroundFloorTrigger> backgroundFloorManagerList; // The list of every background ground (ladder)
    private Animator animator; // The player animator
    [SerializeField] AttackManager attackManager;

    private float horizontalMouvement;
    private float verticalMouvement;

    private Vector3 velocity = Vector3.zero;
    private BomusSlotManager bomusSlotManager;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (groundCheckLeft == null || groundCheckRight == null || rb == null || spriteRendererPlayer == null || rotationManager == null || animator == null || attackManager == null)
        {
            Debug.LogError("SerializeField Missing");
        }

        bomusSlotManager = FindObjectOfType<BomusSlotManager>();
        if(bomusSlotManager == null)
        {
            Debug.LogError("NO bomus Slot manger found in UI");
        }


        foreach (GameObject backgroundfloor in GameObject.FindGameObjectsWithTag("BackgroundFloor"))
        {
            backgroundFloorTrigger bgft = backgroundfloor.GetComponent<backgroundFloorTrigger>();
            if (bgft == null)
            {
                Debug.LogAssertion("backgroundFloorTrigger is not found in " + backgroundfloor.name + " at " + backgroundfloor.transform.position);
            }
            else
            {
                backgroundFloorManagerList.Add(bgft);
            }
            
        }
    }
    void Update()
    {
        if(rotationManager.isRotating) { return; } // if the game is rotating the player isn't able to move/jump.


        // ==== Mouvement player such as jump and moving left/right ==== //
        // == Check if the player is on ground == //
        Collider2D hit = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        if(hit != null && hit.tag != "NotGround" && hit.tag != "BackgroundFloor")
        {
            rb.gravityScale = 1;
            isGrounded = true;
            nbJump = 0;
        }
            
        else
            isGrounded = false;

        



        // === Calculate the force the player should have on his rigidbody === //
        horizontalMouvement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMouvement = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;
        
        

        // == flip the sprite of the player considering his mouvement
        if(horizontalMouvement > 0)
        {
            
            spriteRendererPlayer.flipX = false;
        }
        else if(horizontalMouvement < 0)
        {
            spriteRendererPlayer.flipX = true;
        }
        facingRight = !spriteRendererPlayer.flipX;
        attackManager.changeOffSet(facingRight);



        // === Detection of basic mouvement ===//
        if (hasDoubleJump == true && Input.GetKeyDown(KeyCode.W) && isJumping == false && inRangeToClimb == false) // jumping
        {

            if(nbJump < 2)
            {
                isJumping = true;
                nbJump += 1;
            }
            
        }


        else if (hasDoubleJump == false && Input.GetKeyDown(KeyCode.W) && isGrounded && isJumping == false && inRangeToClimb == false) // jumping
        {
            isJumping = true;
        }

        else if (Input.GetKeyDown(KeyCode.W) && inRangeToClimb) // climbing
        {
            isClimbing = true;
            FindObjectOfType<AudioManager>().Play("climb");
            if (Input.GetKeyDown(KeyCode.S)) // unclimbing
            {
                foreach (backgroundFloorTrigger bgft in backgroundFloorManagerList)
                {
                    bgft.DisactiveGround();
                }

            }
        }


        if (isClimbing)
        {
            rb.gravityScale = 0; // the actual disable gravity
            if (Mathf.Abs(rb.velocity.y) > 0.3)
            {

                // the player is climbing the ladder
            }
            else
            {

                // the player is on the ladder but not climbing
            }
        }

        if (Input.GetAxis("Fire1") != 0f && isGrounded && attackManager.canAttack) // attack
        {
            animator.SetBool("Attack", true);
            attackManager.Attack(this);
            FindObjectOfType<AudioManager>().Play("sword_attack");
        }
        else
        {
            animator.SetBool("Attack", false);
            if (rb.velocity.y < -0.7 && !isClimbing)  // The player is actually having vertical speed (down)
            {
                if(hasSlowFalling)
                {
                    rb.gravityScale = 0.25f;
                }
                else
                {
                    rb.gravityScale = 1f;
                }
                animator.SetBool("Fall", true);
            }
            else
            {
                animator.SetBool("Fall", false);
                if(rb.velocity.y > 0.5 && !isClimbing)
                {
                    animator.SetBool("Jump", true); // The player is actually having vertical speed (up)
                }
                else
                {
                    animator.SetBool("Jump", false); // The player is actually having vertical speed (up)
                    animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
                }
                
            }
        }



        // === Do the animation === //

        

        



    }

    private void FixedUpdate()
    {
        if (rotationManager.isRotating) { return; } // if the game is rotating the player isn't able to move/jump.

        // Applied force on the player
        MovePlayer(horizontalMouvement, verticalMouvement);




    }


    /// <summary>
    /// Move the player in 2D space.
    /// Also include climbing and jumping
    /// </summary>
    public void MovePlayer(float _horizontalMouvement,float _verticalMouvement)
    {
        Vector3 targetVelocity;
        if(isClimbing) 
        {
            targetVelocity = new Vector2(_horizontalMouvement, _verticalMouvement); // we applied vertical force using the player input and not the gravity
        }
        else
        {
            targetVelocity = new Vector2(_horizontalMouvement, rb.velocity.y); // We applied normal gravity
        }
        
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f); // We change the velocity

        if (isJumping) // applied vertical force if jumping
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
        

    }

    public void EnableDoubleJump()
    {
        
        hasDoubleJump = true;
        StartCoroutine(StopDoubleJump());
    }

    private IEnumerator StopDoubleJump()
    {
        
        for (int i = 0; i < 15;i++)
        {
            bomusSlotManager.SetDoubleJump(1f * i,15);
            yield return new WaitForSeconds(1f);
        }
        hasDoubleJump = false;
        bomusSlotManager.DisableJumpUI();
    }

    public void HasSword()
    {
        bomusSlotManager.SetSword();
    }

    public void EnableSlowFalling()
    {
        hasSlowFalling = true;
      
        StartCoroutine(StopSlowFalling());
    }

    private IEnumerator StopSlowFalling()
    {
        
        for (int i = 0; i < 15; i++)
        {
            bomusSlotManager.SetSlowFalling(1f * i,15);
            yield return new WaitForSeconds(1f);
        }
        hasSlowFalling = false;
        bomusSlotManager.DisableFallingUI();
    }
}
