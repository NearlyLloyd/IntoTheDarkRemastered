using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject cam;
    public float pickupRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickupRange))
            {
                if (hit.collider.gameObject.tag == "Interactable")
                {
                    hit.collider.gameObject.GetComponent<StorageItem>().interact();
                }
            }
        }


    }
}
