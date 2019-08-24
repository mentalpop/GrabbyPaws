using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using UnityEngine;

public class ComputerTrigger : MonoBehaviour {

    public vThirdPersonController player;
    public Camera playerCamera;
    [SerializeField]
    private Camera myCamera;
    private bool inCol;

	// Use this for initialization
	void Start () {
        myCamera = GetComponentInChildren<Camera>();
        inCol = false;
        myCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {


        if(inCol && Input.GetKeyDown(KeyCode.E))
        {
            if (myCamera.enabled == false)
            {
                myCamera.enabled = true;
                playerCamera.enabled = false;
                player.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inCol = true;
            Debug.Log("Hello");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inCol = false;
        }
    }


    public void exitScreen()
    {
        myCamera.enabled = false;
        playerCamera.enabled = true;
        player.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
