using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    [SerializeField] private string sceneName;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
