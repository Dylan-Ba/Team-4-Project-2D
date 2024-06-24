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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
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
}
