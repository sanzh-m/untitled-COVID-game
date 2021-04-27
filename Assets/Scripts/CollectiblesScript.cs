using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesScript : MonoBehaviour
{

    [SerializeField] private int counter = 0;
    [SerializeField] private Text counterText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            counter++;
            counterText.text = counter.ToString();
            
        }
    }
}
