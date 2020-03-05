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
    private void Start()
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
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("DoorBoxCollider"))
        {
            ai.Instance.ChangeNavigationState(Constants.NavState.toPlayer, this.transform.position);
            Debug.Log("outcollider");

        }
    }
}
