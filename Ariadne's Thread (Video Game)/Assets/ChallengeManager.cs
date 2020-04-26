using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeManager : NarrationManager
{
    public NarrationDialogue incorrectAnswer;
    // Singleton pattern declaration.
    private static ChallengeManager instance = null;
    public static ChallengeManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;

        // Initiate queue.
        dialogues = new Queue<NarrationDialogue>();
       

    }
    
    
    public void DisplayIncorrectAnswer()
    {
        DeactivateButtons();
        StartCoroutine(GenerateDialogue(incorrectAnswer));
        dialogues.Clear();
    }
    protected override void AddListenerToDisplayIncorrectAnswerMessage(Button button)
    {
        button.onClick.AddListener(DisplayIncorrectAnswer);
    }
    protected override void AddListenerToDestroyTrigger(Button button)
    {
        button.onClick.AddListener(Player.Instance.DestroyCurrentDoor);
    }


}
