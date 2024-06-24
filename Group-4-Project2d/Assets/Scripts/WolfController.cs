using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    private int maxHealth = 3;
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
