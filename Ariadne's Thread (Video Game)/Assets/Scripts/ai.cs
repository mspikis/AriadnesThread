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
    private GameObject closestDoor;
    private Vector3 doorPosition;
    private Renderer rend;


    public GameObject canvas;

    // Player position
    Transform player;

    // NavMesh
    NavMeshAgent nav;
    private int navState; 


    void Start()
    {
        //Set up references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = this.GetComponent<NavMeshAgent>();
        rend = this.GetComponent<Renderer>();
        navState = 1;

    }

    void LateUpdate()
    {
        // Gets positions
        closestDoor = FindClosestDoor();
        if (closestDoor)
        {
            doorPosition = closestDoor.transform.position;

            float playerDoorDist = Vector3.Distance(player.position, doorPosition);

            // When player far from door State 1
            if (playerDoorDist > 15)
            {
                navState = 1;
            }
            // When player close to door
            else
            {
                // If ai close to door State 3
                if (Vector3.Distance(gameObject.transform.position, doorPosition) < 5f)
                {
                    navState = 3;
                }
                // If ai far from door State 2
                else
                {
                    navState = 2;
                }
            }
        }
        else
        {
            navState = 1;
        }
        ChangeNavigationState(navState);
        // Y axis movement
        nav.baseOffset = averagePoint - Mathf.Cos(Time.time * verticalSpeed) * amplitude;
    }

   
    // Find the closest game object with tag door
    public GameObject FindClosestDoor()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Door");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - player.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }

    // Change ai navigation state
    public void ChangeNavigationState(int stateIndex)
    {
        switch (stateIndex)
        {
            // Roaming
            case 1:
                halo.enabled = false;
                nav.SetDestination(player.position);
                rend.material.SetColor("_MainColor", Constants.BLUE);
                break;
            // Going to door
            case 2:
                nav.SetDestination(doorPosition);
                break;
            // Interactable
            case 3:
                nav.SetDestination(this.transform.position);
                rend.material.SetColor("_MainColor", Constants.YELLOW);
                halo.enabled = true;
                break;

        }
    }

    void OnMouseDown()
    {
        if (navState == 3)
        {
            Debug.Log("canvas");
            canvas.SetActive(true);
        }
    }
    public void DestroyDoor()
    {
        Destroy(closestDoor.gameObject);
    }

}