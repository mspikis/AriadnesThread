using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineTrigger : MonoBehaviour
{
    public NarrationPart storylinePart;

    public void TriggerDialogue ()
    {
       NarrationManager.Instance.StartDialogues(storylinePart, true);
    }
}
