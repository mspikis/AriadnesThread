using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material.SetColor("_MainColor", Constants.BLUE);
    }



}
