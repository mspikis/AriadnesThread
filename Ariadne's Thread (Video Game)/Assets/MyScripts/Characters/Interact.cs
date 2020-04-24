using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Collider currentDoor;
    private static Interact instance = null;
    public static Interact Instance
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
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
               AIController.Instance.ChangeNavigationState(Constants.NavState.Interactable, AIController.Instance.transform.position);
            //AIController.Instance.SetCanvas(collision.transform.parent.gameObject.transform.GetChild(1).gameObject);
            currentDoor = collision;


        }
        
    }

    private void OnTriggeExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            AIController.Instance.ChangeNavigationState(Constants.NavState.toPlayer, Player.Instance.transform.position);
            Debug.Log("State 1");

        }
    }
}
