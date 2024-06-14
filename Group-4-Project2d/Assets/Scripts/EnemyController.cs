using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
