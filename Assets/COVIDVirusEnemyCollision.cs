using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COVIDVirusEnemyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.KillEnemy();
        }
        else if (other.CompareTag("IndestructibleEnemy"))
        {
            IndestructibleEnemy enemy = other.gameObject.GetComponent<IndestructibleEnemy>();
            enemy.KillEnemy();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.KillEnemy();
        } else if (collision.gameObject.CompareTag("IndestructibleEnemy"))
        {
            IndestructibleEnemy enemy = collision.gameObject.GetComponent<IndestructibleEnemy>();
            enemy.KillEnemy();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.KillEnemy();
        } else if (collision.gameObject.CompareTag("IndestructibleEnemy"))
        {
            IndestructibleEnemy enemy = collision.gameObject.GetComponent<IndestructibleEnemy>();
            enemy.KillEnemy();
        }
    }

}
