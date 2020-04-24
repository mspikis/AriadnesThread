using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeTrigger : MonoBehaviour
{
    public NarrationPart challengePart;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material.SetColor("_MainColor", Constants.BLUE);
    }

    public void TriggerDialogue()
    {
        NarrationManager.Instance.StartDialogues(challengePart, false);
    }
    

}
