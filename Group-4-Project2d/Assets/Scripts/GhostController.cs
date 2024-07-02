using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float kbForce;
    public float kbCounter;
    public float kbTotalTime;
    public bool knockFromRight;

    public Animator Animator;

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
        if (player.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            knockFromRight = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            knockFromRight = false;
        }

        if (kbCounter <= 0)
        { 
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (knockFromRight == true)
            {
                rb.velocity = new Vector2(-kbForce, 5);
            }
            if (knockFromRight == false)
            {
                rb.velocity = new Vector2(kbForce, 5);
            }
            kbCounter -= Time.deltaTime;
        }
        
    }
    private void FixedUpdate()
    {
        
    }
    public void TakeDamage(int damage)
    {
        kbCounter = kbTotalTime;
        currentHealth -= damage;
        Animator.SetBool("Was hit", true);
        Debug.Log("Enemy has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            AudioManager.Instance.GhostDie();
            Invoke("HandleDeath", 0.5f);
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

            kbCounter = kbTotalTime;
            
        }
    }
    private void HandleDeath()
    {
        gm.ghostKilled++;
        Destroy(gameObject);
    }
}
