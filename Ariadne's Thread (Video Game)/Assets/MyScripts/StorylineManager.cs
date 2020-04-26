using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorylineManager : NarrationManager
{
    // Singleton pattern declaration.
    private static StorylineManager instance = null;
    public static StorylineManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
   
        // Initiate queue.
        dialogues = new Queue<NarrationDialogue>();
        
    }

}
