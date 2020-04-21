using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NarrationManager : MonoBehaviour
{
    public Text dialogueText;
    public Button[] buttons;

    private Queue<NarrationDialogue> dialogues;
    // Singleton
    private static NarrationManager instance = null;
    public static NarrationManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogues = new Queue<NarrationDialogue>();
    }

    public void StartDialogues (NarrationPart narrationPart)
    {
        dialogues.Clear();

        foreach (NarrationDialogue dialogue in narrationPart.dialogues)
        {
            dialogues.Enqueue(dialogue);
        }

        DisplayNextDialogue();  
    }

    public void DisplayNextDialogue ()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }
        NarrationDialogue dialogue = dialogues.Dequeue();
        StopAllCoroutines();
        StartCoroutine(GenerateDialogue(dialogue));
        CreateButtons(dialogue);


    }

    IEnumerator GenerateDialogue (NarrationDialogue dialogue)
    {
        string sentence = dialogue.sentence;

        dialogueText.text = "";
        ai.Instance.talking = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        ai.Instance.talking = false;
        StartCoroutine(ai.Instance.FWToNormal());
    }

    void CreateButtons(NarrationDialogue dialogue)
    {
        if (dialogue.answers.Length != 0)
        {
            int i = 0;
            foreach (string answer in dialogue.answers)
            {
                if (i < 3)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = answer;
                }
                i++;
            }
        }
        else
        {
            buttons[0].gameObject.SetActive(true);
            buttons[0].gameObject.transform.GetChild(0).GetComponent<Text>().text = "Continue";
        }

    }

    void EndDialogue()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

}
