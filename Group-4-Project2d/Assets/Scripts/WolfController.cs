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
    // Start is called before the first frame update
    void Start()
    {
        biteAttack.gameObject.SetActive(false);
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        ghostPrefab.gameObject.SetActive(false);

    }

    private void Update()
    {
        HandleMovement();
        HandleBite();

        ghostPrefab.transform.position = new Vector2(transform.position.x, transform.position.y +1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Invisible Wall"))
        {
            Debug.Log("Invisible Wall");
            if (transform.localScale.x > 0)
            {
                speed = speed * -1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.localScale.x < 0)
            {
                speed = speed * -1;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
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
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            ghostPrefab.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void HandleMovement()
    {
        if (gm.kbCounter <= 0)
        {

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoint[0].position) < 0.2f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 1;
                }
            }
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoint[1].position) < 0.2f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 0;
                }
            }
        }
     
        else
        {
            if (gm.knockFromRight == true)
            {
                rb.velocity = new Vector2(gm.kbForce, 5);
            }
            if (gm.knockFromRight == false)
            {
                rb.velocity = new Vector2(-gm.kbForce, 5);
            }
            gm.kbCounter -= Time.deltaTime;
        }
        
    }

    private void HandleBite()
    {
        playerDistance = Vector2.Distance(attackPoint.transform.position, player.transform.position);
        if (playerDistance <= 1f)
        {
            biteAttack.gameObject.SetActive(true);

            Invoke("StopBite", 0.3f);
        }
    }
    private void StartBite()
    {
        biteAttack.gameObject.SetActive(true);
        Invoke("StopBite", 0.3f);
    }
    private void StopBite()
    {
        biteAttack.gameObject.SetActive(false);

    }


}
