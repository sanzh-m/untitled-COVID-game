using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public int[] counts;
    public TextMeshProUGUI[] counters;

    public TextMeshProUGUI[] requirementDisplayers;

    // Start is called before the first frame update
    void Start()
    {
        if (counts.Length != counters.Length || counts.Length != requirementDisplayers.Length)
            throw new System.Exception("Requirement and count lengths should be the same");
        for (int i = 0; i < counts.Length; ++i)
        {
            requirementDisplayers[i].text = counts[i].ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var satisfied = true;
            for (int i = 0; i < counts.Length; ++i)
            {
                if (counts[i] > int.Parse(counters[i].text))
                {
                    satisfied = false;
                }
            }

            var title = satisfied
                ? "Congrats!"
                : "Almost there";
            var body = satisfied
                ? "Congratulations on finishing the level!"
                : "Well done for reaching here, but you don't have enough items collected";
            var button = satisfied
                ? "Move to the next scene!"
                : "Ok";
            EditorUtility.DisplayDialog(title, body, button);
            if (satisfied) SceneManager.LoadScene(sceneName);
        }
    }
}