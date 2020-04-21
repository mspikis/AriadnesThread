using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    public NarrationPart narrationPart;

    public void TriggerDialogue ()
    {
       NarrationManager.Instance.StartDialogues(narrationPart);
    }
}
