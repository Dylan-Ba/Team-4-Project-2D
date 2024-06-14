using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Inputs


    //Physics
    Vector2 move;
    Vector2 jump;
    public float runSpeed;
    public float jumpSpeed;

    [Range(0f, 1f)]
    public float groundDecay;

    public bool grounded;
    float xInput;
    float yInput;

    //Game Objects
    public GameManager gameManager;
    Rigidbody2D rb;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        HandleRun();
        HandleJump();
        
        //Vector2 direction = new Vector2(xInput, yInput);
        //rb.velocity = direction * runSpeed;
    }

    private void FixedUpdate()
    {
        Friction();
    }

    void GetInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }
    void HandleRun()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            rb.velocity = new Vector2(xInput * runSpeed, rb.velocity.y);
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            Debug.Log("JUMP");
        }
    }
    void Friction()
    {
        if (grounded && Input.GetAxis("Horizontal") == 0 && rb.velocity.y <=0)
        {
            rb.velocity *= groundDecay;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;

        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;

        }
    }
}
