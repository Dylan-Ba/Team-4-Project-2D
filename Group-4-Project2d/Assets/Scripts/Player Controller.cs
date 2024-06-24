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
    public GameManager gm;
    Rigidbody2D rb;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public LayerMask wolfMask;
    public LayerMask ghostMask;
    public GameObject sword;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage = 1;

    public int playerHealth;

    public Animator Animator;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sword.gameObject.SetActive(false);
        initialGravity = rb.gravityScale;
        gliding = false;
        playerHealth = 3;

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

        
        
        playerHealth = gm.currentHealth;
        Animator.SetInteger("Health", playerHealth);
        
        if (playerHealth < 1)
        {
            Invoke("HandleDeath", 1.25f);
        }
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
        if (gm.kbCounter <= 0)
        {
            if (Mathf.Abs(xInput) > 0)
            {

                rb.velocity = new Vector2(xInput * runSpeed, rb.velocity.y);
                float direction = Mathf.Sign(xInput);
                transform.localScale = new Vector3(direction, 1, 1);
                Animator.SetFloat("Speed", Mathf.Abs(direction));

            }
            else
            {
                Animator.SetFloat("Speed", 0);
            }
        }
        else
        {
            if (gm.knockFromRight == true)
            {
                rb.velocity= new Vector2(-gm.kbForce, 5);
            }
            if (gm.knockFromRight == false)
            {
                rb.velocity=new Vector2(gm.kbForce, 5);
            }
            gm.kbCounter -= Time.deltaTime;
        }
        
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            AudioManager.Instance.Jump(); //Plays the "jump" sound
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
                Animator.SetBool("Gliding", true);
                Gravity();
                rb.velocity = new Vector2(xInput * glideSpeed, glideFall);
            }
            else if (!grounded && gliding)
            {
                gliding = false;
                Animator.SetBool("Gliding", false);
                Gravity();
            }
        }

        
    }
    private void HandleMelee()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grounded)
            {
                sword.gameObject.SetActive(true);
                Animator.SetBool("Is Attacking", true);

                Collider2D[] hitWolf = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, wolfMask);

                foreach (Collider2D enemy in hitWolf)
                {
                    enemy.GetComponent<WolfController>().TakeDamage(attackDamage);
                    AudioManager.Instance.Hit();  //Plays the "hit" sound
                }

                Collider2D[] hitGhost = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, ghostMask);

                foreach (Collider2D enemy in hitGhost)
                {
                    enemy.GetComponent<GhostController>().TakeDamage(attackDamage);
                    AudioManager.Instance.Hit();
                }
                Invoke("SetSwordFalse", 0.5f);
            }
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
            Animator.SetBool("Grounded", true);

        }
        if (other.gameObject.tag == "Deathplane")
        {
            HandleDeath();
        }

       if (other.gameObject.tag == "Attack")
        {
            gm.currentHealth--;

        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
            Animator.SetBool("Grounded", false);
        }
    }

    private void SetSwordFalse()
    {
        sword.gameObject.SetActive(false);
        Animator.SetBool("Is Attacking", false );
    }
    private void HandleDeath()
    {
        Debug.Log("Death!!!");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        gm.currentHealth = 3;
        gm.ghostKilled = 0;
    }
}
