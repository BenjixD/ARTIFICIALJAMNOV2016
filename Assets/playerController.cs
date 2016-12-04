using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    playerStats ps;

    // left and right movement variables
    float move;
    public float moveSpeed = 10f;
    bool facingRight = true;

    // jumping variables
    public float jumpVelocity = 10f;
    public float offWallVelocity = 10f;
    bool canJump = true;
    bool canDoubleJump = true;
    bool inWallJump = false;
    public float wallJumpDuration = 0.1f;

    // variables to check if player is grounded
    bool grounded = false;
    public Transform groundCheck;
    public float checkRadius = 0.1f;
    public LayerMask whatIsGround;

    // variables to check if player is hooked
    bool hooked = false;
    bool underHooked = false;
    public Transform hookCheck;
    public Transform underHookCheck;
    public float wallCheckRadius = 0.1f;
    public LayerMask whatIsWall;

    bool attacking = false;
    int attackNum = 0;


    void Start()
    {
        // sets Rigidbody2D rb as the rigidbody component of the gameobject this script is attached to
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        ps = this.gameObject.GetComponent<playerStats>();
    }


    void Update()
    {
        // this block controls when the player can jump
        if (canJump && !hooked && Input.GetAxis("Jump") > 0)
        {
            Jump();
        }
        if (canJump && hooked && Input.GetAxis("Jump") > 0)
        {
            WallJump();
        }
        if (!grounded && canDoubleJump && Input.GetAxis("Jump") == 0)
        {
            canDoubleJump = false;
            canJump = true;
        }
        if (grounded)
        {
            canJump = true;
            canDoubleJump = true;
        }
        if (hooked)
        {
            canDoubleJump = true;
        }
        if(underHooked)
        {
            canDoubleJump = true;
            if (Input.GetAxis("Jump") > 0)
            {
                stickUnder();
            }
        }

        // prevents player from sliding  the dragon
        if (grounded && Input.GetAxis("Jump") == 0 && (!(hooked && !(Input.GetAxis("Horizontal") == 0))))
        {
            stayOn();
        }

        // controls when player can shield
        if(Input.GetAxis("Shield") > 0 && ps.playerShield > 0)
        {
            ps.shieldActive = true;
            //Debug.Log("Shield is active!");
        }
        else
        {
            ps.shieldActive = false;
            //Debug.Log("Shield is not active...");
        }

        // controls when player can attack
        if(Input.GetButtonDown("Attack"))
        {
            Attack();
        }
    }


    void FixedUpdate()
    {
        // this block controls player's left and right movement
        if (((!attacking && grounded) || !grounded) && !inWallJump && !underHooked)
        {
            move = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
        }

        // this block controls when the player's sprite is flipped horizontally
        if (move < 0 && facingRight)
        {
            Flip();
        }
        else if (move > 0 && !facingRight)
        {
            Flip();
        }

        

        // updates Bool grounded to whether the player is standing on the dragon or not
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // updates bool hooked to whether the player is hooked on the dragon or not
        hooked = Physics2D.OverlapCircle(hookCheck.position, wallCheckRadius, whatIsWall);
        underHooked = Physics2D.OverlapCircle(underHookCheck.position, wallCheckRadius, whatIsWall);
    }


    // flips horizontal scale of object
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = this.gameObject.GetComponent<Transform>().localScale;
        theScale.x *= -1;
        this.gameObject.GetComponent<Transform>().localScale = theScale;
    }


    // makes the player jump
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        canJump = false;

    }

    void WallJump()
    {
        canJump = false;
        inWallJump = true;
        if (hooked && !facingRight)
        {
            rb.velocity = new Vector2(offWallVelocity, jumpVelocity);
        }
        else if (hooked && facingRight)
        {
            rb.velocity = new Vector2(-1 * offWallVelocity, jumpVelocity);
        }
        Invoke("notWallJump", wallJumpDuration);

    }

    void stickUnder()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    }

    void stayOn()
    {
        rb.velocity = new Vector2(rb.velocity.x, -5);
    }

    // makes the player attack in the direction it is currently facing
    void Attack()
    {
        attackNum++;
        Debug.Log("Player attacked " + attackNum + " times!");
    }

    void notWallJump()
    {
        inWallJump = false;
    }
}
