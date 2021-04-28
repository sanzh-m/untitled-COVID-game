using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneChange : MonoBehaviour
{

    [SerializeField] private string sceneName;
    public int[] counts;
    public GameObject[] elements;
    // private Dictionary<GameObject>
    Dictionary<string, int> requirements = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        if (counts.Length != elements.Length)
            throw new System.Exception("Requirement and count lengths should be the same");
        for (int i = 0; i < counts.Length; ++i)
        {
            if (requirements.ContainsKey(elements[i].name))
                throw new System.Exception("Elements should be unique");
            requirements[elements[i].name] = counts[i];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject[] collectibleCounts = GameObject.FindGameObjectsWithTag("CollectibleCount");
            bool satisfied = true;
            foreach (GameObject collectibleCount in collectibleCounts) {
                if (requirements[collectibleCount.GetComponent<CollectibleCount>().element.name] > int.Parse(collectibleCount.GetComponent<UnityEngine.UI.Text>().text)) {
                    satisfied = false;
                }
            }
            EditorUtility.DisplayDialog(satisfied.ToString(), "Place", "Do Not Place");
            // SceneManager.LoadScene(sceneName);
        }
    }
}
