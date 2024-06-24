using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float maxHealth = 3;
    public float currentHealth;

    public GameObject player;
    public float speed;

    private float distance;

    public GameManager gm;
    [SerializeField]
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (gm.kbCounter >= 0)
        { 
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
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
    private void FixedUpdate()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            gm.ghostKilled++;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            gm.kbCounter = gm.kbTotalTime;
            if(other.transform.position.x <= transform.position.x)
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
}
