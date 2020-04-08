using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance = null;

    public static Player Instance
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
        if (collision.gameObject.CompareTag("DoorBoxCollider"))
        {
            ai.Instance.ChangeNavigationState(Constants.NavState.toDoor, collision.transform.parent.transform.position);
            Debug.Log("incollider");


        }
        if (collision.gameObject.CompareTag("NarrationPart"))
        {
            
            if (collision.gameObject.name == "Trigger 1")
            {
                ai.Instance.ChangeNavigationState(Constants.NavState.toPlayer, Player.Instance.transform.position);
                StartCoroutine(StartNarration(collision, 3.0f));
            }
            else
            {
                StartCoroutine(StartNarration(collision, 0f));
            }



        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("DoorBoxCollider"))
        {
            ai.Instance.ChangeNavigationState(Constants.NavState.toPlayer, this.transform.position);

        }
    }

    private IEnumerator StartNarration(Collider col, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        NarrationManager.Instance.transform.GetChild(0).gameObject.SetActive(true);
        col.gameObject.GetComponent<NarrationTrigger>().TriggerDialogue();
        Destroy(col.gameObject);
        StopAllCoroutines();
    }
}
