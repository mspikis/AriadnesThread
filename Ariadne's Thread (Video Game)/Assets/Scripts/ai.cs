using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour
{
    public float verticalSpeed = 1.8f;
    public float averagePoint = 1.2f;
    public float amplitude = 0.2f;
    public Behaviour halo;
    private Vector3 doorPosition;
    private Renderer rend;
    Transform player;
    NavMeshAgent nav;


    void Awake()
    {
        //Set up references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = this.GetComponent<NavMeshAgent>();
        rend = this.GetComponent<Renderer>();

    }

    void FixedUpdate()
    {
        nav.baseOffset = averagePoint - Mathf.Cos(Time.time * verticalSpeed) * amplitude;

        doorPosition = FindClosestDoor().transform.position;

        float playerDoorDist = Vector3.Distance(player.position, GameObject.FindGameObjectWithTag("Door").transform.position);

        if (playerDoorDist > 15)
        {
            ChangeState(1);
            Debug.Log("1");
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, doorPosition)< 5f)
            {
                ChangeState(3);
                Debug.Log("3");
            }
            else
            {
                ChangeState(2);
                Debug.Log("2");
            }
        }
        // Hovering
        nav.baseOffset = averagePoint - Mathf.Cos(Time.time * verticalSpeed) * amplitude;
    }

   

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

    public void ChangeState(int stateIndex)
    {
        switch (stateIndex)
        {
            // Roaming
            case 1:
                halo.enabled = false;
                nav.SetDestination(player.position);
                rend.material.SetColor("_MainColor", Color.red);
                break;
            // Going to door
            case 2:
                nav.SetDestination(doorPosition);
                break;
            // Interactable
            case 3:
                nav.SetDestination(this.transform.position);
                rend.material.SetColor("_MainColor", Color.yellow);
                halo.enabled = true; 
                break;

        }
    }
}