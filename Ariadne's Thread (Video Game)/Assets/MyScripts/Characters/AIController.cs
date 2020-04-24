using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // Hovering variables
    public float verticalSpeed = 1.8f;
    public float averagePoint = 1.2f;
    public float amplitude = 0.2f;

    // Components 
    private Renderer rend;

    


    // NavMesh
    NavMeshAgent nav;
    private bool followingPlayer;

    // AI Talking
    public bool talking = false;
    private bool increasingFW = true;
    public float FWincreament = 0.03f;
    public float FWDefault = 1.0f;
    


    public Constants.NavState navState;

    // Singleton
    private static AIController instance = null;
    public static AIController Instance
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

    void Start()
    {
        
        //Set up references.
        nav = this.GetComponent<NavMeshAgent>();
        rend = this.GetComponent<Renderer>();
        rend.material.SetFloat("_FresnelWidth", FWDefault);
        navState = Constants.NavState.idle;
        ChangeNavigationState(navState, this.transform.position);
    }

    void Update()
    {
        if (followingPlayer)
        {
            nav.SetDestination(Player.Instance.transform.position);
        }
        if (talking)
        {
                if (rend.material.GetFloat("_FresnelWidth") > FWDefault + Random.Range(0.9f, 1.7f))
                {
                    increasingFW = false;
                }
                else if (rend.material.GetFloat("_FresnelWidth") < FWDefault + FWincreament )
                {
                    increasingFW = true;
                }
                if (increasingFW)
                {
                    rend.material.SetFloat("_FresnelWidth", rend.material.GetFloat("_FresnelWidth") + FWincreament);
                }
                else
                {
                    rend.material.SetFloat("_FresnelWidth", rend.material.GetFloat("_FresnelWidth") - FWincreament);
                }
        }

        nav.baseOffset = averagePoint - Mathf.Cos(Time.time * verticalSpeed) * amplitude;
    }

   
    // Change ai navigation state
    public void ChangeNavigationState(Constants.NavState newNavSate, Vector3 targetPos)
    {
        switch (newNavSate)
        {
            // Idle
            case Constants.NavState.idle:
                followingPlayer = false;
                break;
            // Following Player
            case Constants.NavState.toPlayer:
                followingPlayer = true;
                rend.material.SetColor("_MainColor", Constants.BLUE);
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                break;
            // Going to door
            case Constants.NavState.toDoor:
                followingPlayer = false;
                break;
            // Interactable
            case Constants.NavState.Interactable:
                followingPlayer = false;
                rend.material.SetColor("_MainColor", Constants.YELLOW);
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                break;

        }
        nav.SetDestination(targetPos);
        navState = newNavSate;
    }
   
    public void SetFollowingPlayer(bool newFollowPlayer)
    {
        followingPlayer = newFollowPlayer;
    }
   
    public IEnumerator FWToNormal()
    {
        while (rend.material.GetFloat("_FresnelWidth") > 0.94f)
        {
                rend.material.SetFloat("_FresnelWidth", rend.material.GetFloat("_FresnelWidth") - FWincreament);
            yield return 0;
        }
        StopAllCoroutines();
    }


}