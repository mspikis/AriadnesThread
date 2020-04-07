using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour
{
    // Hovering variables
    public float verticalSpeed = 1.8f;
    public float averagePoint = 1.2f;
    public float amplitude = 0.2f;

    // Components 
    public Behaviour halo;
    private Renderer rend;

    // Challenge canvas
    private GameObject canvas;


    // NavMesh
    NavMeshAgent nav;
    private bool followingPlayer = false;

    public Constants.NavState navState;

    // Singleton
    private static ai instance = null;
    public static ai Instance
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
        navState = Constants.NavState.idle;
        ChangeNavigationState(navState, this.transform.position);
    }

    void Update()
    {
        if (followingPlayer)
        {
            nav.SetDestination(Player.Instance.transform.position);
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
                halo.enabled = false;
                followingPlayer = true;
                rend.material.SetColor("_MainColor", Constants.BLUE);
                break;
            // Going to door
            case Constants.NavState.toDoor:
                followingPlayer = false;
                break;
            // Interactable
            case Constants.NavState.Interactable:
                followingPlayer = false;
                rend.material.SetColor("_MainColor", Constants.YELLOW);
                halo.enabled = true;
                break;

        }
        nav.SetDestination(targetPos);
        navState = newNavSate;
    }
    public void SetCanvas(GameObject newCanvas)
    {
        canvas = newCanvas;
    }
    public void SetFollowingPlayer(bool newFollowPlayer)
    {
        followingPlayer = newFollowPlayer;
    }
    private void OnMouseDown()
    {
        if (navState == Constants.NavState.Interactable)
        {
            Debug.Log("canvas");
            canvas.SetActive(true);
        }
    }


}