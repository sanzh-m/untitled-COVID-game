using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHelper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // collision.gameObject.GetComponent<PlayerController>().enabled = false;
            // collision.gameObject.GetComponent<PlayerController>().enabled = false;
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.Fall();
        }
    }
}
