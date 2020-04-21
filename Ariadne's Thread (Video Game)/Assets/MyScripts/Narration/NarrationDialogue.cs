using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NarrationDialogue
{
    [TextArea(3,10)]
    public string sentence;
    [TextArea(0, 1)]
    public string[] answers;
}
