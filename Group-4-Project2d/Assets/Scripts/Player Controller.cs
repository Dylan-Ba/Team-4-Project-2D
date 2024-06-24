using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Inputs


    //Physics
    Vector2 move;
    Vector2 jump;
    public float runSpeed;
    public float jumpSpeed;
    public float glideSpeed;
    public float glideFall;
    private float initialGravity;

    [Range(0f, 1f)]
    public float groundDecay;

    public bool grounded;
    public bool gliding;
    float xInput;
    float yInput;

    //Game Objects
    public GameManager gameManager;
    Rigidbody2D rb;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public LayerMask enemyMask;
    public GameObject sword;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage = 40;

    public int playerHealth;
   

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        sword.gameObject.SetActive(false);
        initialGravity = rb.gravityScale;
        gliding = false;

    }

    // Update is called once per frame
    void Update()
    {


        GetInputs();
        HandleRun();
        HandleJump();
        HandleMelee();
        HandleGlide();
        //HandleDeath();
        
        //Vector2 direction = new Vector2(xInput, yInput);
        //rb.velocity = direction * runSpeed;
    }

    private void FixedUpdate()
    {
        Friction();

        if (grounded)
        {
            rb.gravityScale = initialGravity;
        }
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
    void HandleGlide()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!grounded && !gliding)
            {
                gliding = true;
                Gravity();
                rb.velocity = new Vector2(xInput * glideSpeed, glideFall);
            }
            else if (!grounded && gliding)
            {
                gliding = false;
                Gravity();
            }
        }

        
    }
    private void HandleMelee()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            sword.gameObject.SetActive(true);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<WolfController>().TakeDamage(attackDamage);
            }
            Invoke("SetSwordFalse", 0.5f);
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void Friction()
    {
        if (grounded && Input.GetAxis("Horizontal") == 0 && rb.velocity.y <=0)
        {
            rb.velocity *= groundDecay;
        }
    }

    void Gravity()
    {
        if (!gliding)
        {
            rb.gravityScale = initialGravity;
        }
        else if (gliding)
        {
            rb.gravityScale = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
            gliding = false;

        }
        if (other.gameObject.tag == "Deathplane")
        {
            HandleDeath();
        }

       if (other.gameObject.tag == "Attack")
        {
            gameManager.currentHealth--;

        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void SetSwordFalse()
    {
        sword.gameObject.SetActive(false);
    }
    private void HandleDeath()
    {
        Debug.Log("Death!!!");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
