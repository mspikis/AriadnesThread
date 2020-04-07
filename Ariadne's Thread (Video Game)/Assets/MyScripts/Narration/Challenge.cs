using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{

    public void DestroyObject()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        ai.Instance.ChangeNavigationState(Constants.NavState.toPlayer, new Vector3(0, 0, 0));
    }

}

