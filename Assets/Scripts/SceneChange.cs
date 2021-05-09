using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public int[] counts;
    public TextMeshProUGUI[] counters;
    public TextMeshProUGUI[] requirementDisplayers;
    public string[] requirementNames;
    public GameObject levelEndDialog;

    // Start is called before the first frame update
    void Start()
    {
        if (counts.Length != counters.Length || counts.Length != requirementDisplayers.Length)
            throw new Exception("Requirement and count lengths should be the same");
        for (int i = 0; i < counts.Length; ++i)
        {
            requirementDisplayers[i].text = counts[i].ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            var satisfied = true;
            var unsatisfied = new ArrayList();
            for (int i = 0; i < counts.Length; ++i)
            {
                if (counts[i] > int.Parse(counters[i].text))
                {
                    satisfied = false;
                    unsatisfied.Add(requirementNames[i]);
                }
            }

            levelEndDialog.SetActive(true);
            var body = levelEndDialog.GetComponentInChildren<Text>(true);
            var buttons = levelEndDialog.GetComponentsInChildren<Button>(true);
            Debug.Log(buttons);
            Debug.Log(body);
            //if (body == null || body.name != "BodyText" || buttons == null || buttons.Length != 2 ||
             //   buttons[0].name != "ContinueButton" ||
              //  buttons[1].name != "OkButton") throw new Exception("Level End Dialog is misconfigured");

            if (satisfied) buttons[0].gameObject.SetActive(true);
            else buttons[1].gameObject.SetActive(true);
            body.text = satisfied
                ? "Congratulations on finishing the level!"
                : "Well done for reaching here, but you don't have enough items collected. You need to collect some " +
                  string.Join(", ", unsatisfied.GetRange(0, unsatisfied.Count - 1).Cast<string>()) + " and " +
                  (string) unsatisfied[unsatisfied.Count - 1] + ". Come back when you are done!";
        }
    }
}