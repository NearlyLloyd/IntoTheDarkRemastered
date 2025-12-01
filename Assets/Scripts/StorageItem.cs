using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StorageItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int storageSize = 0;
    public GameObject storageUI;
    public GameObject player;
    private Movement moveScript;
    private bool open = false;

    private float tempSens;
    private float tempSpeed;
    //private float tempMoveSmooth;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && open && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            Close();
        }
    }

    public void Interact()
    {
        if (open)
        {
            Close();
        }
        else
        {
            open = true;
            storageUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            tempSens = moveScript.lookSensitivity;
            tempSpeed = moveScript.moveSpeed;
            moveScript.lookSensitivity = 0;
            moveScript.moveSpeed = 0;
            
            Debug.Log("storage interacted with");
        }


       
    }

    public void Close()
    {
        open = false;
        storageUI.SetActive(false);
        moveScript.lookSensitivity = tempSens;
        moveScript.moveSpeed = tempSpeed;
        //moveScript.MoveSmoothTime = tempMoveSmooth;

        Cursor.lockState = CursorLockMode.Locked;

    }

    
}
