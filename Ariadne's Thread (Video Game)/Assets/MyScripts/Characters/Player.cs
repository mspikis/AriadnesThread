using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public FirstPersonController characterController;
    public bool storyMode;
    public GameObject storylinePanel;
    public GameObject challengePanel;

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
        characterController = GetComponent<FirstPersonController>();
        storyMode = false;
    }
    private void Update()
    {
        InteractWithAI();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DoorBoxCollider"))
        {
            AIController.Instance.ChangeNavigationState(Constants.NavState.toDoor, collision.transform.parent.transform.position);
            Debug.Log("incollider");


        }
        if (collision.gameObject.CompareTag("NarrationPart"))
        {
            
            if (collision.gameObject.name == "StorylineTrigger1")
            {
                AIController.Instance.ChangeNavigationState(Constants.NavState.toPlayer, Player.Instance.transform.position);
                StartCoroutine(StartStorylinePart(collision, 3.0f));
            }
            else
            {
                StartCoroutine(StartStorylinePart(collision, 0f));
            }



        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("DoorBoxCollider"))
        {
            AIController.Instance.ChangeNavigationState(Constants.NavState.toPlayer, Player.Instance.transform.position);

        }
    }

        private void InteractWithAI()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (AIController.Instance.navState == Constants.NavState.Interactable && Player.Instance.storyMode == false)
            {

               
                StartChallenge(Interact.Instance.currentDoor);

            }
        }
    }
    
    private IEnumerator StartStorylinePart(Collider currentStoryPart, float delayTime)
    {
        DisableMovement();
        yield return new WaitForSeconds(delayTime);
        storylinePanel.gameObject.SetActive(true);

        currentStoryPart.gameObject.GetComponent<StorylineTrigger>().TriggerDialogue();
        
        Destroy(currentStoryPart.gameObject);
        StopAllCoroutines();
        
    }
 
    private void StartChallenge(Collider currentChallenge)
    {
        challengePanel.gameObject.SetActive(true);
        currentChallenge.gameObject.GetComponent<ChallengeTrigger>().TriggerDialogue();
        DisableMovement();
    }


    public void DisableMovement()
    {
        characterController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        storyMode = true;
    }

    public void EnableMovement()
    {
        characterController.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        storyMode = false;
        
    }
    public void DestroyCurrentDoor()
    {
        Destroy(Interact.Instance.currentDoor.gameObject);
        AIController.Instance.ChangeNavigationState(Constants.NavState.toPlayer, Player.Instance.transform.position);
    }

}
