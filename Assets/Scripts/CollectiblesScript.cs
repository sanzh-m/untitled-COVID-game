using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectiblesScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI counterText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            counterText.text = (int.Parse(counterText.text) + 1).ToString();
            
        }
    }
}
