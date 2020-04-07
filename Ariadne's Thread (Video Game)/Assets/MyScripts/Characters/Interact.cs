using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            ai.Instance.ChangeNavigationState(Constants.NavState.Interactable, ai.Instance.transform.position);
            ai.Instance.SetCanvas(collision.transform.parent.gameObject.transform.GetChild(1).gameObject);

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("State 0");

        }
    }

    private void OnTriggeExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            ai.Instance.ChangeNavigationState(Constants.NavState.toPlayer, Player.Instance.transform.position);
            Debug.Log("State 1");

        }
    }
}
