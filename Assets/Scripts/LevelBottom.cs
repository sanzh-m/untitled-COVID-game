using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBottom : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameOverUI.SetActive(true);
        } else if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.KillEnemy();
        } else if (collision.gameObject.tag == "IndestructibleEnemy")
        {
            IndestructibleEnemy enemy = collision.gameObject.GetComponent<IndestructibleEnemy>();
            enemy.KillEnemy();
        }
    }
}
