using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{
    public GameObject storylinePanel;
    public Text storylineText;
    public Button[] storylineButtons;

    public GameObject challengePanel;
    public Text challengeText;
    public Button[] challengeButtons;

    private GameObject panelActive;
    private Text textActive;
    private Button[] buttonsActive;

    public float charSpeed = 0.05f;
 


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

    public void StartDialogues (NarrationPart narrationPart, bool isStoryline)
    {
        dialogues.Clear();

        foreach (NarrationDialogue dialogue in narrationPart.dialogues)
        {
            dialogues.Enqueue(dialogue);
        }

        AssignActivePanelProperties(isStoryline);
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue ()
    {
        
        

        foreach (Button button in buttonsActive)
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
        


    }

    IEnumerator GenerateDialogue (NarrationDialogue dialogue)
    {
        string sentence = dialogue.sentence;
        textActive.text = "";
        AIController.Instance.talking = true;
        foreach (char letter in sentence.ToCharArray())
        {
            textActive.text += letter;
            yield return new WaitForSeconds(charSpeed);
        }
        AIController.Instance.talking = false;
        StartCoroutine(AIController.Instance.FWToNormal());
        CreateButtons(dialogue);
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
                   buttonsActive[i].gameObject.SetActive(true);
                   buttonsActive[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = answer;
                }
                i++;
            }
        }
        else
        {
           buttonsActive[0].gameObject.SetActive(true);
           buttonsActive[0].gameObject.transform.GetChild(0).GetComponent<Text>().text = "Continue";
        }

    }

    void EndDialogue()
    {
        panelActive.gameObject.SetActive(false);
        Player.Instance.EnableMovement();
    }



    private void AssignActivePanelProperties(bool isStoryline)
    {
        if (isStoryline)
        {
            buttonsActive = storylineButtons;
            textActive = storylineText;
            panelActive = storylinePanel;
        }
        else
        {
            buttonsActive = challengeButtons;
            textActive = challengeText;
            panelActive = challengePanel;
        }
    }
}
