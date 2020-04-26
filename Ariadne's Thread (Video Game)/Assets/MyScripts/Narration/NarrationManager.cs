using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NarrationManager : MonoBehaviour
{
    // Assign corresponding narration panels.
    [SerializeField]
    protected Text textActive;
    [SerializeField]
    protected Button[] buttonsActive;

    // Time elapsed between the display of each character.
    private float charSpeed = 0.001f;
 

    // Queue for storring the dialogue parts.
    protected Queue<NarrationDialogue> dialogues;

    

    

    // Loads the dialogues into the dialogue queue from the NarrationTriggers
    public void StartDialogues (NarrationPart narrationPart)
    {
        // Clear previous queue.
        dialogues.Clear();
        // Load each dialogue part in the queue.
        foreach (NarrationDialogue dialogue in narrationPart.dialogues)
        {
            dialogues.Enqueue(dialogue);
        }

        // AssignActivePanelProperties(isStoryline);
        DisplayNextDialogue();
    }

    // Display next dialogue 
    public void DisplayNextDialogue ()
    {
        // Disable all the buttons
        DeactivateButtons();
        // If no more dialogue parts left in the queue end the dialogue and stop the execution.
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }
        // Go to the next dialogue part.
        NarrationDialogue dialogue = dialogues.Dequeue();
        StopAllCoroutines();
        // Display dialogue part text.
        StartCoroutine(GenerateDialogue(dialogue));
        


    }

    // Generate dialogue text.
    protected IEnumerator GenerateDialogue (NarrationDialogue dialogue)
    {
        string sentence = dialogue.sentence;
        textActive.text = "";
        // Start AI talking animation.
        AIController.Instance.talking = true;
        // Display each character with a delay
        foreach (char letter in sentence.ToCharArray())
        {
            textActive.text += letter;
            yield return new WaitForSeconds(charSpeed);
        }
        // Stop AI talking animation.
        AIController.Instance.talking = false;
        StartCoroutine(AIController.Instance.FWToNormal());
        // Generate buttons.
        CreateButtons(dialogue);
    }
    // Generate buttons.
    protected void CreateButtons(NarrationDialogue dialogue)
    {
        // If there are answers then generate up to three buttons.
        if (dialogue.myButtons.Length != 0)
        {
            int i = 0;
            foreach (MyButton button in dialogue.myButtons)
            {
                if (i < 3)
                {
                   buttonsActive[i].gameObject.SetActive(true);
                   buttonsActive[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = button.answer;
                    if (button.isFalse)
                    {
                        AddListenerToDisplayIncorrectAnswerMessage(buttonsActive[i]);
                    }
                    else
                    {
                        buttonsActive[i].onClick.AddListener(DisplayNextDialogue);
                        AddListenerToDestroyTrigger(buttonsActive[i]);

                    }
                }
                i++;
            }
        }
        // If no answers are available then create only one continue button.
        else
        {
           buttonsActive[0].gameObject.SetActive(true);
           buttonsActive[0].gameObject.transform.GetChild(0).GetComponent<Text>().text = "Continue";
            buttonsActive[0].onClick.AddListener(DisplayNextDialogue);
        }

    }
    // End the dialogue.
    public void EndDialogue()
    {
        // Deactivate the panel.
        this.gameObject.SetActive(false);
        // Enable player movement.
        Player.Instance.EnableMovement();
    }



    protected void DeactivateButtons()
    {
        foreach (Button button in buttonsActive)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
    protected virtual void AddListenerToDisplayIncorrectAnswerMessage(Button button)
    {
        
    }
    protected virtual void AddListenerToDestroyTrigger(Button button)
    {

    }

}
