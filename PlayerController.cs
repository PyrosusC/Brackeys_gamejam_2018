using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sr;

    [Header("Horizontal Movement options")]
    public float speed = 5f;    //Movement speed

    [Header("Jump options")]
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce = 5f;
    public int extraJumpsAmount = 1;    //Extra in air jumps amount (e.g value 1 gives ability to perform another jump midair, known as "double jump")

    [Header("Ground Checker options")]
    public float groundCheckerY = 0.5f;    //Set this value to half of player collider height 
    public float groundCheckerRadius = 0.7f;    //Set this value to player collider width
    public LayerMask groundLayer;

    //Other variables
    float moveFloat;
    bool isRight = true;
    bool onGround;
    int extJump;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        extJump = extraJumpsAmount;
    }

    private void FixedUpdate()
    {
        //Sets boolean by circle checker
        onGround = Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(0f, groundCheckerY * -1, 0f), groundCheckerRadius, groundLayer);

        //Horizontal movement
        rb.velocity = new Vector2(moveFloat * speed, rb.velocity.y);
    }

    private void Update()
    {
        //Gets player input
        moveFloat = Input.GetAxis("Horizontal");

        //Checks if player is on ground
        if (onGround == true)
        {
            //Debug.Log("On Ground");       //Uncomment this line while struggling with setting "Ground Checker" Radius & Y Position
            extJump = extraJumpsAmount;
        }

        //Flips sprite
        if (moveFloat < 0 && isRight == false)
        {
            sr.flipX = false;
            isRight = !isRight;
        }
        else if (moveFloat > 0 && isRight == true)
        {
            sr.flipX = true;
            isRight = !isRight;
        }

        //Performs extra jump
        if (Input.GetKeyDown(jumpKey) && extJump > 0)
        {
            rb.velocity = Vector2.up * jumpForce;            //rb.AddForce(Vector2.up * jumpForce);
            extJump--;
        }
        //Performs jump
        else if (onGround && Input.GetKeyDown(jumpKey) && extJump == 0)
        {
            rb.velocity = Vector2.up * jumpForce;            //rb.AddForce(Vector2.up * jumpForce);
        }
    }
}
