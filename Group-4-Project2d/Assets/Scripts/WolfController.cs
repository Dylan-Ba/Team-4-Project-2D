using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    private int maxHealth = 3;
    [SerializeField]
    private int currentHealth;
    public GameObject ghostPrefab;
    private Vector2 pos;
    public float speed;
    public float maxSpeed;
    public Rigidbody2D rb;
    [SerializeField]
    private LayerMask playerMask;

    public GameObject player;
    private float playerDistance;
    public GameManager gm;


    public Transform[] patrolPoint;
    public int patrolDestination;


    public GameObject biteAttack;
    public GameObject attackPoint;
    public float biteRange;

    public float kbForce;
    public float kbCounter;
    public float kbTotalTime;
    public bool knockFromRight;

    public Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        speed = maxSpeed;
        Animator.SetFloat("WalkSpeed", speed);
        biteAttack.gameObject.SetActive(false);
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        ghostPrefab.gameObject.SetActive(false);
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    private void Update()
    {
        HandleMovement();
        HandleBite();

        ghostPrefab.transform.position = new Vector2(transform.position.x, transform.position.y +1);

        if (player.transform.position.x - transform.position.x > 0)
        {
            knockFromRight = false;
        }
        else 
        {
            knockFromRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject == player)
        {
            gm.kbCounter = gm.kbTotalTime;
            if (other.transform.position.x <= transform.position.x)
            {
                gm.knockFromRight = true;
            }
            if (other.transform.position.x >= transform.position.x)
            {
                gm.knockFromRight = false;
            }
            gm.currentHealth--;
        }
        if (other.gameObject.tag == "Deathplane")
        {
            TakeDamage(3);
        }
    }

    public void TakeDamage(int damage)
    {
        kbCounter = kbTotalTime;
        Animator.SetBool("WasHit", true);
        Invoke("EndHitAnim", 0.5f);
        currentHealth -= damage;
        Debug.Log("Enemy has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            Invoke("OnDeath", 0.5f);
        }
    }

    private void HandleMovement()
    {
        if (kbCounter <= 0)
        {

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoint[0].position) < 0.2f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 1;
                }
            }
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoint[1].position) < 0.2f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 0;
                }
            }
        }
     
        else
        {
            if (knockFromRight == true)
            {
                rb.velocity = new Vector2(kbForce, 5);
            }
            if (knockFromRight == false)
            {
                rb.velocity = new Vector2(-kbForce, 5);
            }
            kbCounter -= Time.deltaTime;
        }
        
    }

    private void HandleBite()
    {
        playerDistance = Vector2.Distance(attackPoint.transform.position, player.transform.position);
        if (playerDistance <= 1f)
        {
            speed = 0;

            StartBite();

            Invoke("StopBite", 0.3f);
        }
    }
    private void StartBite()
    {
        Animator.SetBool("Attacking", true);
        biteAttack.gameObject.SetActive(true);
        Invoke("StopBite", 0.3f);
    }
    private void StopBite()
    {
        speed = maxSpeed;
        Animator.SetBool("Attacking", false);
        biteAttack.gameObject.SetActive(false);
    }
    private void EndHitAnim()
    {
        Animator.SetBool("WasHit", false);
    }
    private void OnDeath()
    {
        ghostPrefab.SetActive(true );
        Destroy(gameObject);
    }

}
