using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFinishedScript : MonoBehaviour
{
    public string sceneName;
    public GameObject levelFinishedUI;
    public GameObject okButton;
    
    public void NextLevel()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1.0f;
    }

    public void OkButton()
    {
        Time.timeScale = 1.0f;
        levelFinishedUI.SetActive(false);
        okButton.SetActive(false);
    }
}
